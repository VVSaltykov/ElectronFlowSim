﻿using System.Diagnostics;
using System.Text;
using ElectronFlowSim.Common;
using Microsoft.AspNetCore.SignalR;

namespace ElectronFlowSim.AnalysisService.Services.ElectronFlow;

/// <summary>
/// Сервис для работы с .exe
/// </summary>
public class ElectronFlowService : IElectronFlowService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    private readonly ILogger<ElectronFlowService> _logger;
    private readonly string _watchDirectory;
    private readonly string _finishedFileName = "finished.txt";

    public ElectronFlowService(IHubContext<NotificationHub> hubContext, ILogger<ElectronFlowService> logger, IConfiguration configuration)
    {
        _hubContext = hubContext;
        _logger = logger;

        _watchDirectory = configuration["DirectoryPaths:WatchDirectory"];

        if (string.IsNullOrEmpty(_watchDirectory))
            throw new InvalidOperationException("Excel file path is not configured.");
    }

    /// <summary>
    /// Запуск .exe
    /// </summary>
    /// <param name="inputFileContent"></param>
    /// <param name="requestId"></param>
    /// <param name="connectionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task RunExecutableAsync(string inputFileContent,
        string requestId,
        string connectionId,
        CancellationToken cancellationToken)
    {
        try
        {
            const float MaxAllowedCpuUsage = 30.0f;

            //await WaitUntilCpuIsCalmAsync(MaxAllowedCpuUsage, cancellationToken: cancellationToken);

            var sessionDir = Path.Combine(_watchDirectory, requestId);
            Directory.CreateDirectory(sessionDir);

            var originalExe = Path.Combine(_watchDirectory, "888.exe");
            var originalTxt = Path.Combine(_watchDirectory, "A2_test");

            var exeCopy = Path.Combine(sessionDir, "888.exe");
            var txtCopy = Path.Combine(sessionDir, "A2_test");
            var finishedPath = Path.Combine(sessionDir, _finishedFileName);

            File.Copy(originalExe, exeCopy);

            string fileContent = PrepareFileContent(inputFileContent);

            await File.WriteAllTextAsync(txtCopy, fileContent, new UTF8Encoding(false));

            SetCompatibilitySettings(exeCopy);

            var startInfo = new ProcessStartInfo
            {
                FileName = exeCopy,
                WorkingDirectory = sessionDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = startInfo };

            // Обработка вывода
            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                    _logger.LogInformation("[stdout] {line}", e.Data);
            };

            process.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                    _logger.LogWarning("[stderr] {line}", e.Data);
            };

            process.Start();

            _logger.LogInformation("Процесс запущен (PID {pid}) из директории {dir}", process.Id, sessionDir);

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            _ = MonitorFinishedFileAsync(process, finishedPath, requestId, connectionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при запуске процесса");
        }
    }

    /// <summary>
    /// Мониторинг завершения .exe
    /// </summary>
    /// <param name="process"></param>
    /// <param name="finishedFilePath"></param>
    /// <param name="sessionId"></param>
    /// <param name="connectionId"></param>
    /// <returns></returns>
    private async Task MonitorFinishedFileAsync(Process process, string finishedFilePath, string sessionId, string connectionId)
    {
        while (!process.HasExited)
        {
            try
            {
                if (File.Exists(finishedFilePath))
                {
                    _logger.LogInformation("Обнаружен файл 'finished.txt' для PID {pid}", process.Id);

                    try
                    {
                        process.Kill();
                        _logger.LogInformation("Процесс {pid} завершён после появления файла.", process.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Не удалось завершить процесс {pid}", process.Id);
                    }

                    File.Delete(finishedFilePath);

                    await _hubContext.Clients.Client(connectionId)
                        .SendAsync("ElectronFlowCompleted", new
                        {
                            SessionId = sessionId,
                            Status = "completed",
                            Timestamp = DateTime.UtcNow
                        });

                    break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при мониторинге файла для PID {pid}", process.Id);
            }

            await Task.Delay(1000);
        }
    }

    /// <summary>
    /// Установка настроек для запуска .exe
    /// </summary>
    /// <param name="exePath"></param>
    private void SetCompatibilitySettings(string exePath)
    {
        const string keyPath = @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";
        using var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyPath);
        key?.SetValue(exePath, "WIN95 256COLOR", Microsoft.Win32.RegistryValueKind.String);
    }

    /// <summary>
    /// Метод для подготовки входящего файла
    /// </summary>
    /// <param name="rawContent"></param>
    /// <returns></returns>
    private string PrepareFileContent(string rawContent)
    {
        string content = rawContent
            .Replace("\\r\\n", "\r\n")
            .Replace("\"", "")
            .Replace("\\n", "\n")
            .TrimEnd();

        if (content.EndsWith("FINISH\r\n\u001A"))
            return content;

        if (content.EndsWith("FINISH"))
            content = content[..^6].TrimEnd();

        return content + "\r\nFINISH\r\n\u001A";
    }

    ///// <summary>
    ///// Ожидание разгрузки процессора
    ///// </summary>
    ///// <param name="maxAllowedCpuUsage"></param>
    ///// <param name="checkIntervalMs"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns></returns>
    ///// <exception cref="OperationCanceledException"></exception>
    //private async Task WaitUntilCpuIsCalmAsync(float maxAllowedCpuUsage, int checkIntervalMs = 1000, CancellationToken cancellationToken = default)
    //{
    //    while (!cancellationToken.IsCancellationRequested)
    //    {
    //        float currentCpuUsage = await GetCpuUsageAsync();

    //        if (currentCpuUsage < maxAllowedCpuUsage)
    //        {
    //            _logger.LogInformation("✅ CPU спокоен: {currentCpuUsage}% (< {maxAllowedCpuUsage}%)", currentCpuUsage, maxAllowedCpuUsage);
    //            return;
    //        }

    //        _logger.LogWarning("⚠️ CPU перегружен: {currentCpuUsage}%. Ждём...", currentCpuUsage);
    //        await Task.Delay(checkIntervalMs, cancellationToken);
    //    }

    //    throw new OperationCanceledException("Мониторинг CPU прерван");
    //}

    ///// <summary>
    ///// Получение данных о состоянии процессора
    ///// </summary>
    ///// <returns></returns>
    //private async Task<float> GetCpuUsageAsync()
    //{
    //    return await Task.Run(() =>
    //    {
    //        try
    //        {
    //            using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
    //            {
    //                cpuCounter.NextValue();
    //                Thread.Sleep(1000);
    //                return cpuCounter.NextValue();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Ошибка при измерении загрузки CPU");
    //            return 100f;
    //        }
    //    });
    //}
}

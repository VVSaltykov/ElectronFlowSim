using System.Globalization;
using System.Text.RegularExpressions;
using Electronflow;
using ElectronFlowSim.DTO.AnalysisService;
using Grpc.Core;
using CalculationParameters = ElectronFlowSim.DTO.AnalysisService.OutputData.CalculationParameters;
using FinalParameters = ElectronFlowSim.DTO.AnalysisService.OutputData.FinalParameters;
using FinalResult = ElectronFlowSim.DTO.AnalysisService.OutputData.FinalResult;
using MicrowaveData = ElectronFlowSim.DTO.AnalysisService.OutputData.MicrowaveData;
using TrajectoryPoint = ElectronFlowSim.DTO.AnalysisService.OutputData.TrajectoryPoint;
using UekvipEntry = ElectronFlowSim.DTO.AnalysisService.OutputData.UekvipEntry;

namespace ElectronFlowSim.AnalysisService.GRPC.Services;

public class ElectronFlowGrpcService : ElectronFlow.ElectronFlowBase
{
    private readonly string _outFilePath;

    public ElectronFlowGrpcService(IConfiguration configuration)
    {
        _outFilePath = configuration["FilePaths:OutFilePath"];

        if (string.IsNullOrEmpty(_outFilePath))
            throw new InvalidOperationException("Excel file path is not configured.");
    }

    /// <summary>
    /// Получение данных из файла с выходными данными из .exe
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<OutputDataResponse> ParseFile(FileRequest request, ServerCallContext context)
    {
        OutputDataDTO parsed = await ParseOutputDataAsync(request.Folderid);

        var response = new OutputDataResponse
        {
            FinalParams = new Electronflow.FinalParameters
            {
                Ig = parsed.FinalParams.IG,
                Km = parsed.FinalParams.KM,
                Kp = parsed.FinalParams.KP,
                Kq = parsed.FinalParams.KQ,
                Nl = parsed.FinalParams.NL,
                Rk = parsed.FinalParams.RK,
                U0 = parsed.FinalParams.U0,
                Bnorm = parsed.FinalParams.BNORM,
                Abm = parsed.FinalParams.ABM,
                Zcentr = parsed.FinalParams.ZCENTR,
                Icr = parsed.FinalParams.ICR,
                Tck = parsed.FinalParams.TCK,
            },
            CathodeDensity = new Electronflow.CathodeCurrentDensity
            {
                Ajtacm =
                {
                    parsed.CathodeDensity.AjtacmRows.Select(row => new DoubleArray
                    {
                        Values = { row }
                    })
                }
            },
            BmData = new Electronflow.BMData
            {
                Rows =
                {
                    parsed.BmData.Rows.Select(row => new DoubleArray
                    {
                        Values = { row }
                    })
                }
            },
            CalcParams = new Electronflow.CalculationParameters
            {
                Dz = parsed.CalcParams.DZ,
                Dtok = parsed.CalcParams.DTOK,
                Ht = parsed.CalcParams.HT,
                Dmaxmm = parsed.CalcParams.DMAXMM,
                L0 = parsed.CalcParams.L0,
            },
            Result = new Electronflow.FinalResult
            {
                TunnelBeamCurrent = parsed.Result.TunnelBeamCurrent,
                TrajectorySteps = parsed.Result.TrajectorySteps,
                Nl1 = parsed.Result.NL1,
                Nl4 = parsed.Result.NL4,
            }
        };

        response.TrajectoryPoints.AddRange(parsed.TrajectoryPoints.Select(p => new Electronflow.TrajectoryPoint
        {
            I = p.I,
            R = p.R,
            Z = p.Z,
            U = p.U,
            Absl = p.ABSL,
            L = p.L,
        }));

        response.UekvipList.AddRange(parsed.UekvipList.Select(p => new Electronflow.UekvipEntry
        {
            I = p.I,
            Value = p.Value
        }));

        response.MicrowavePoints.AddRange(parsed.MicrowavePoints.Select(p => new Electronflow.MicrowaveData
        {
            Microperevance = p.Microperevance,
            BeamCurrent = p.BeamCurrent
        }));

        response.EkvList.AddRange(parsed.EkvList.Select(p => new Electronflow.EkvData
        {
            I = p.I,
            Orekv = p.OREKV,
            Ozekv = p.OZEKV
        }));

        response.EkvPointList.AddRange(parsed.EkvPointList.Select(p => new Electronflow.EkvPointData
        {
            K = p.K,
            Orep = p.OREP,
            Ozep = p.OZEP
        }));

        return response;
    }

    /// <summary>
    /// Парсер выходного файла
    /// </summary>
    /// <param name="folderId"></param>
    /// <returns></returns>
    /// <exception cref="RpcException"></exception>
    private async Task<OutputDataDTO> ParseOutputDataAsync(string folderId)
    {
        var path = Path.Combine($"{_outFilePath}/{folderId}", "out");
        if (!File.Exists(path))
            throw new RpcException(new Status(StatusCode.NotFound, $"Файл out в {folderId} не найден"));

        var output = new OutputDataDTO
        {
            BmData = new DTO.AnalysisService.OutputData.BMData { Rows = new() },
            FinalParams = new FinalParameters(),
            CathodeDensity = new DTO.AnalysisService.OutputData.CathodeCurrentDensity { AjtacmRows = new() },
            Result = new FinalResult(),
            UekvipList = new List<DTO.AnalysisService.OutputData.UekvipEntry>(),
            MicrowavePoints = new List<DTO.AnalysisService.OutputData.MicrowaveData>(),
            TrajectoryPoints = new List<DTO.AnalysisService.OutputData.TrajectoryPoint>(),
            EkvList = new List<DTO.AnalysisService.OutputData.EkvData>(),
            EkvPointList = new List<DTO.AnalysisService.OutputData.EkvPointData>(),
        };

        var lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            if (line.StartsWith("  I="))
            {
                var match = Regex.Match(line,
                    @"I=\s*(\d+)\s+R=\s*([\d.+-Ee]+)\s+Z=\s*([\d.+-Ee]+)\s+U=\s*([\d.+-Ee]+)\s+ABSL\(I\)=\s*(\d+)\s+L=\s*(\d+)");
                if (match.Success)
                {
                    output.TrajectoryPoints.Add(new TrajectoryPoint()
                    {
                        I = int.Parse(match.Groups[1].Value),
                        R = double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture),
                        Z = double.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture),
                        U = double.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture),
                        ABSL = int.Parse(match.Groups[5].Value),
                        L = int.Parse(match.Groups[6].Value)
                    });
                }
            }

            else if (line.Contains("IG=") && line.Contains("ZCENTR="))
            {
                var match = Regex.Match(line,
                    @"IG=(\d+)\s+KM=\s*(\d+)\s+KP=\s*(\d+)\s+KQ=\s*(\d+)\s+NL=\s*(\d+)\s+RK=\s*([\d.]+)\s+U0=\s*([\d.]+)\s+BNORM=\s*([\d.]+)\s+ZCENTR=\s*([\d.EeDd+-]+)");
                if (match.Success)
                {
                    output.FinalParams.IG = int.Parse(match.Groups[1].Value);
                    output.FinalParams.KM = int.Parse(match.Groups[2].Value);
                    output.FinalParams.KP = int.Parse(match.Groups[3].Value);
                    output.FinalParams.KQ = int.Parse(match.Groups[4].Value);
                    output.FinalParams.NL = int.Parse(match.Groups[5].Value);
                    output.FinalParams.RK = double.Parse(match.Groups[6].Value, CultureInfo.InvariantCulture);
                    output.FinalParams.U0 = double.Parse(match.Groups[7].Value, CultureInfo.InvariantCulture);
                    output.FinalParams.BNORM = double.Parse(match.Groups[8].Value, CultureInfo.InvariantCulture);
                    output.FinalParams.ZCENTR = double.Parse(match.Groups[9].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                }
            }

            else if (line.Contains("ICR=") && line.Contains("TCK=") && line.Contains("ABM="))
            {
                var match = Regex.Match(line, @"ICR=\s*(\d+)\s+TCK=\s*([\d.EeDd+-]+)(?:\s+ABM=\s*([\d.EeDd+-]+))?");
                if (match.Success)
                {
                    output.FinalParams.ICR = int.Parse(match.Groups[1].Value);
                    output.FinalParams.TCK = double.Parse(match.Groups[2].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    output.FinalParams.ABM = double.Parse(match.Groups[3].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                }
            }

            else if (line.Contains("UEKVIP(I)="))
            {
                var match = Regex.Match(line, @"I=\s*(\d+)\s+UEKVIP\(I\)=\s*([\d.EeDd+-]+)");
                if (match.Success)
                {
                    int index = int.Parse(match.Groups[1].Value);
                    double value = double.Parse(match.Groups[2].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    output.UekvipList.Add(new UekvipEntry() { I = index, Value = value });
                }
            }

            else if (line.Contains("MICROPERVEANCE=") && line.Contains("BEAM CURRENT="))
            {
                var match = Regex.Match(line, @"MICROPERVEANCE=\s*([\d.EeDd+-]+)\s+BEAM CURRENT=\s*([\d.EeDd+-]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    double mp = double.Parse(match.Groups[1].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    double bc = double.Parse(match.Groups[2].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    output.MicrowavePoints.Add(new MicrowaveData() { Microperevance = mp, BeamCurrent = bc });
                }
            }

            else if (line.Contains("BM="))
            {
                var cleanLine = line.Substring(line.IndexOf("BM=") + "BM=".Length);
                var matches = Regex.Matches(cleanLine, @"[-+]?\d*\.?\d+(?:[EeDd][-+]?\d+)?");

                var row = new List<double>();
                foreach (Match m in matches)
                {
                    var valStr = m.Value.Replace("D", "E");
                    if (double.TryParse(valStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                        row.Add(val);
                }

                if (row.Count > 0)
                    output.BmData.Rows.Add(row);
            }

            else if (Regex.IsMatch(line, @"^\s*\d+\s+[-+]?\d*\.?\d+"))
            {
                var matches = Regex.Matches(line, @"[-+]?\d*\.?\d+(?:[EeDd][-+]?\d+)?");
                var row = new List<double>();
                foreach (Match m in matches)
                {
                    var valStr = m.Value.Replace("D", "E");
                    if (double.TryParse(valStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                        row.Add(val);
                }

                if (row.Count > 0)
                    output.BmData.Rows.Add(row);
            }

            else if (line.Contains("DZ=") && line.Contains("DTOK="))
            {
                var match = Regex.Match(line,
                    @"DZ=\s*([\d.EeDd+-]+)\s*DTOK=\s*([\d.EeDd+-]+)\s*HT=\s*([\d.EeDd+-]+)\s*DMAXMM=\s*([\d.EeDd+-]+)");
                if (match.Success)
                {
                    output.CalcParams = new DTO.AnalysisService.OutputData.CalculationParameters()
                    {
                        DZ = double.Parse(match.Groups[1].Value.Replace("D", "E"), CultureInfo.InvariantCulture),
                        DTOK = double.Parse(match.Groups[2].Value.Replace("D", "E"), CultureInfo.InvariantCulture),
                        HT = double.Parse(match.Groups[3].Value.Replace("D", "E"), CultureInfo.InvariantCulture),
                        DMAXMM = double.Parse(match.Groups[4].Value.Replace("D", "E"), CultureInfo.InvariantCulture)
                    };
                }
            }

            else if (line.Contains("L0="))
            {
                var match = Regex.Match(line, @"L0=\s*(\d+)");
                if (match.Success)
                {
                    output.CalcParams ??= new CalculationParameters();
                    output.CalcParams.L0 = int.Parse(match.Groups[1].Value);
                }
            }

            else if (line.Contains("AJTACM="))
            {
                var matches = Regex.Matches(line, @"[-+]?\d*\.\d+D[+-]?\d+");
                var row = new List<double>();
                foreach (Match m in matches)
                {
                    var valStr = m.Value.Replace("D", "E");
                    if (double.TryParse(valStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                        row.Add(val);
                }
                if (row.Count > 0)
                    output.CathodeDensity.AjtacmRows.Add(row);
            }

            else if (line.Contains("BEAM CURRENT IN TUNNEL"))
            {
                var match = Regex.Match(line, @"BEAM CURRENT IN TUNNEL\s*=\s*([\d.,ED+-]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var numStr = match.Groups[1].Value.Replace("D", "E").Replace(",", ".").Trim();
                    if (double.TryParse(numStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
                    {
                        output.Result.TunnelBeamCurrent = val;
                    }
                }
            }

            else if (line.Contains("TRAJECTORIES CALCULATION IS FINISHED"))
            {
                var match = Regex.Match(line, @"FINISHED,\s*(\d+)STEPS", RegexOptions.IgnoreCase);
                if (match.Success)
                    output.Result.TrajectorySteps = int.Parse(match.Groups[1].Value);

                var nlMatch = Regex.Match(line, @"NL1,NL4=\s*(\d+)\s+(\d+)");
                if (nlMatch.Success)
                {
                    output.Result.NL1 = int.Parse(nlMatch.Groups[1].Value);
                    output.Result.NL4 = int.Parse(nlMatch.Groups[2].Value);
                }
                else if (i + 1 < lines.Length && lines[i + 1].Contains("NL1,NL4="))
                {
                    nlMatch = Regex.Match(lines[i + 1], @"NL1,NL4=\s*(\d+)\s+(\d+)");
                    if (nlMatch.Success)
                    {
                        output.Result.NL1 = int.Parse(nlMatch.Groups[1].Value);
                        output.Result.NL4 = int.Parse(nlMatch.Groups[2].Value);
                        i++;
                    }
                }
            }

            else if (line.Contains("I=") && line.Contains("OREKV="))
            {
                var match = Regex.Match(line, @"I=\s*([\d.EeDd+-]+)\s+OREKV=\s*([\d.EeDd+-]+)\s+OZEKV=\s*([\d.EeDd+-]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    int _i = int.Parse(match.Groups[1].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    double orekv = double.Parse(match.Groups[2].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    double ozekv = double.Parse(match.Groups[3].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    output.EkvList.Add(new DTO.AnalysisService.OutputData.EkvData { I = _i, OREKV = orekv, OZEKV = ozekv });
                }
            }
            else if (line.Contains("K=") && line.Contains("OREP="))
            {
                var match = Regex.Match(line, @"K=\s*([\d.EeDd+-]+)\s+OREP=\s*([\d.EeDd+-]+)\s+OZEP=\s*([\d.EeDd+-]+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    int k = int.Parse(match.Groups[1].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    double orep = double.Parse(match.Groups[2].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    double ozep = double.Parse(match.Groups[3].Value.Replace("D", "E"), CultureInfo.InvariantCulture);
                    output.EkvPointList.Add(new DTO.AnalysisService.OutputData.EkvPointData { K = k, OREP = orep, OZEP = ozep });
                }
            }
        }

        return output;
    }
}


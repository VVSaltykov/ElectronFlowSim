﻿using System.Globalization;
using System.Text;
using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.API.Services.File;

public class TxtFileService : ITxtFileService
{
    /// <summary>
    /// Создание входного файла для запуска .exe
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<string> CreateInputFile(InputDataDTO input)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"ig      {input.ig}");
        sb.AppendLine($"nmas    {input.nmas}");
        sb.AppendLine($"km      {input.km}");
        sb.AppendLine($"kp      {input.kp}");
        sb.AppendLine($"kq      {input.kq}");
        sb.AppendLine($"kpj6    {input.kpj6}");
        sb.AppendLine($"ik      {input.ik}");
        sb.AppendLine($"j1      {input.j1}");
        sb.AppendLine($"icr     {input.icr}");
        sb.AppendLine($"jcr     {input.jcr}");

        sb.AppendLine($"r       {FormatArrayWithLineBreak(input.r, 8)}");
        sb.AppendLine($"z       {FormatArrayWithLineBreak(input.z, 8)}");
        sb.AppendLine($"u       {FormatUArrayWithLineBreaks(input.u, 8)}");
        sb.AppendLine($"l       {FormatArrayWithLineBreak(input.l, 9)}");

        sb.AppendLine($"rk      {input.rk.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"utep    {input.utep.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"zkon    {input.zkon.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"akl1    {input.akl1.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"u0      {input.u0.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"uekvip  {FormatArrayWithLineBreak(input.uekvip, 8)}");
        sb.AppendLine($"bnorm   {input.bnorm.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"abm     {input.abm.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"bm      {FormatArrayWithLineBreak(input.bm, 8)}");
        sb.AppendLine($"aik     {FormatArrayWithLineBreak(input.aik, 8)}");
        sb.AppendLine($"ht      {input.ht.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"dz      {input.dz.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"dtok    {input.dtok.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"hq1     {input.hq1.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLine($"ar1s     {input.ar1s.ToString(CultureInfo.InvariantCulture)}");

        sb.AppendLine("FINISH");
        sb.AppendLine(" ");

        return sb.ToString();
    }

    public Task GetOutputFile(OutputDataDTO outputDataDTO)
    {
        throw new NotImplementedException();
    }

    private string FormatUArrayWithLineBreaks(double[] array, int elementsPerLine)
    {
        if (array == null || array.Length == 0)
            return string.Empty;

        var groupedValues = new List<string>();
        double currentValue = array[0];
        int count = 1;

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] == currentValue)
            {
                count++;
            }
            else
            {
                groupedValues.Add($"{count}({currentValue.ToString("F7", CultureInfo.InvariantCulture)})");
                currentValue = array[i];
                count = 1;
            }
        }
        groupedValues.Add($"{count}({currentValue.ToString("F7", CultureInfo.InvariantCulture)})");

        var result = new StringBuilder();
        for (int i = 0; i < groupedValues.Count; i++)
        {
            result.Append(groupedValues[i]);

            if (i < groupedValues.Count - 1)
                result.Append(",");

            if ((i + 1) % elementsPerLine == 0 && i < groupedValues.Count - 1)
                result.AppendLine();
        }

        return result.ToString();
    }

    private string FormatInlineArray(double[] array)
    {
        return string.Join(",", array.Select(x => x.ToString(CultureInfo.InvariantCulture)));
    }

    private string FormatArrayWithLineBreak(double[] array, int elementsPerLine)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            sb.Append(array[i].ToString(CultureInfo.InvariantCulture));
            if (i < array.Length - 1)
                sb.Append(",");

            if ((i + 1) % elementsPerLine == 0 && i < array.Length - 1)
                sb.AppendLine();
        }
        return sb.ToString();
    }

    private string FormatArrayWithLineBreak(int[] array, int elementsPerLine)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            sb.Append(array[i]);
            if (i < array.Length - 1)
                sb.Append(",");

            if ((i + 1) % elementsPerLine == 0 && i < array.Length - 1)
                sb.AppendLine();
        }
        return sb.ToString();
    }
}
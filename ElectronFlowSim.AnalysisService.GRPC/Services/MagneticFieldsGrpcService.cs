﻿using Electronflow;
using ElectronFlowSim.DTO.AnalysisService;
using ElectronFlowSim.DTO.AnalysisService.Enum;
using Grpc.Core;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ElectronFlowSim.AnalysisService.GRPC.Services;

public class MagneticFieldsGrpcService : MagneticFields.MagneticFieldsBase
{
    public override async Task<MagneticFieldsOutputResponse> GetMagneticFieldsFromFile(MagneticFieldsFileRequest request, ServerCallContext context)
    {
        using var memoryStream = new MemoryStream(request.FileContent.ToByteArray());

        var result = ParseMagneticFieldsFromStream(memoryStream, request.StartPoint, request.EndPoint, request.Step);

        return new MagneticFieldsOutputResponse
        {
            MagneticFieldsPoints = { result.z },
            MagneticFieldsValues = { result.bm },
            MagneticFieldMaxValues = result.bnorm
        };
    }

    public override async Task<NZRUTableDataOutputResponse> GetNZRUTableDataFromFile(NZRUTableDataFileRequest request, ServerCallContext context)
    {
        using var memoryStream = new MemoryStream(request.FileContent.ToByteArray());

        var parsedDataList = ParseNZRUDataFromStream(memoryStream);

        var response = new NZRUTableDataOutputResponse();

        foreach (var electrodeData in parsedDataList)
        {
            var electrodeResponse = new ElectrodeData
            {
                U = electrodeData.U,
                WorkpieceType = electrodeData.WorkpieceType.ToString()
            };

            electrodeResponse.N.AddRange(electrodeData.N);
            electrodeResponse.Z.AddRange(electrodeData.Z);
            electrodeResponse.R.AddRange(electrodeData.R);

            response.Electrodes.Add(electrodeResponse);
        }

        return response;
    }

    public override async Task<NLTableDataOutputResponse> GetNLTableDataFromFile(NLTableDataFileRequest request, ServerCallContext context)
    {
        using var memoryStream = new MemoryStream(request.FileContent.ToByteArray());

        var response = ParseNLDataFromStream(memoryStream);

        var outputResponse = new NLTableDataOutputResponse();
        outputResponse.N.AddRange(response.N);
        outputResponse.L.AddRange(response.L);
        return outputResponse;
    }

    private BMDataDTO ParseMagneticFieldsFromStream(Stream stream, double startPoint, double endPoint, double step)
    {
        try
        {
            var workbook = new XSSFWorkbook(stream);
            var sheet = workbook.GetSheetAt(0);

            var bnorm = sheet.GetRow(1).GetCell(6).NumericCellValue;
            var data = new List<(double z, double b0)>();

            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                if (row == null) continue;

                var zCell = row.GetCell(0);
                var b0Cell = row.GetCell(1);

                if (zCell == null || b0Cell == null) continue;

                double z = zCell.NumericCellValue;
                double b0 = b0Cell.NumericCellValue;

                data.Add((z, b0));
            }

            var filteredData = data
                .Where(d => d.z >= startPoint && d.z <= endPoint)
                .Where((d, index) => index % (int)(step * 10) == 0)
                .OrderBy(d => d.z)
                .ToList();

            return new BMDataDTO
            {
                z = filteredData.Select(d => d.z).ToList(),
                bm = filteredData.Select(d => d.b0).ToList(),
                bnorm = bnorm
            };
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private List<NZRUTableDTO> ParseNZRUDataFromStream(Stream stream)
    {
        var workbook = new XSSFWorkbook(stream);
        var sheet = workbook.GetSheetAt(0);

        var result = new List<NZRUTableDTO>();
        var currentType = WorkpieceType.Cathode;
        var currentData = new NZRUTableDTO
        {
            WorkpieceType = currentType,
            N = new List<int>(),
            Z = new List<double>(),
            R = new List<double>(),
            U = 0
        };

        for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
        {
            var row = sheet.GetRow(rowIndex);
            if (row == null) continue;

            var cellA = row.GetCell(0);
            if (cellA != null && !string.IsNullOrEmpty(cellA.ToString()))
            {
                if (currentData.N.Count > 0)
                {
                    result.Add(currentData);
                }

                var electrodeName = cellA.ToString();
                currentType = electrodeName switch
                {
                    "Катод" => WorkpieceType.Cathode,
                    "Фокусирующий электрод" => WorkpieceType.FocusingElectrode,
                    "Анод 1" => WorkpieceType.Anode1,
                    "Анод 2" => WorkpieceType.Anode2,
                    "Замедляющая система" => WorkpieceType.DeceleratingSystem,
                    _ => throw new Exception($"Неизвестный тип электрода: {electrodeName}")
                };

                currentData = new NZRUTableDTO
                {
                    WorkpieceType = currentType,
                    N = new List<int>(),
                    Z = new List<double>(),
                    R = new List<double>(),
                    U = 0
                };
            }

            var cellB = row.GetCell(1); 
            var cellC = row.GetCell(2); 
            var cellD = row.GetCell(3); 
            var cellE = row.GetCell(4); 

            if (cellB != null && !string.IsNullOrEmpty(cellB.ToString()))
            {
                if (int.TryParse(cellB.ToString(), out int n))
                {
                    currentData.N.Add(n);
                }

                if (cellC != null && double.TryParse(cellC.ToString(), out double z))
                {
                    currentData.Z.Add(z);
                }

                if (cellD != null && double.TryParse(cellD.ToString(), out double r))
                {
                    currentData.R.Add(r);
                }

                if (cellE != null && double.TryParse(cellE.ToString(), out double u) && currentData.U == 0)
                {
                    currentData.U = u;
                }
            }
        }

        if (currentData.N.Count > 0)
        {
            result.Add(currentData);
        }

        return result;
    }

    private NLTableDTO ParseNLDataFromStream(Stream stream)
    {
        var workbook = new XSSFWorkbook(stream);
        var sheet = workbook.GetSheetAt(1);

        var result = new NLTableDTO
        {
            N = new List<int>(),
            L = new List<int>()
        };

        var formulaEvaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();

        for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
        {
            var row = sheet.GetRow(rowIndex);
            if (row == null) continue;

            var cellN = row.GetCell(0);
            var n = GetCellValueAsInt(cellN, formulaEvaluator);

            var cellL = row.GetCell(1);
            var l = GetCellValueAsInt(cellL, formulaEvaluator);

            if (n.HasValue && l.HasValue)
            {
                result.N.Add(n.Value);
                result.L.Add(l.Value);
            }
            else
            {
                Console.WriteLine($"Skipping row {rowIndex}: N={cellN}, L={cellL}");
            }
        }

        return result;
    }

    private int? GetCellValueAsInt(ICell cell, IFormulaEvaluator evaluator)
    {
        if (cell == null) return null;

        try
        {
            if (cell.CellType == CellType.Formula)
            {
                var evaluatedCell = evaluator.Evaluate(cell);
                if (evaluatedCell.CellType == CellType.Numeric)
                {
                    return (int)evaluatedCell.NumberValue;
                }
            }

            if (cell.CellType == CellType.Numeric)
            {
                return (int)cell.NumericCellValue;
            }

            if (int.TryParse(cell.ToString(), out int result))
            {
                return result;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing cell: {ex.Message}");
        }

        return null;
    }
}

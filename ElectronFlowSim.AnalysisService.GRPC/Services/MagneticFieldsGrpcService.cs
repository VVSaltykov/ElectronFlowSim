using Electronflow;
using ElectronFlowSim.DTO.AnalysisService;
using Grpc.Core;
using NPOI.XSSF.UserModel;

namespace ElectronFlowSim.AnalysisService.GRPC.Services;

public class MagneticFieldsGrpcService : MagneticFields.MagneticFieldsBase
{
    public override async Task<OutputResponse> GetMagneticFieldsFromFile(MagneticFieldsFileRequest request, ServerCallContext context)
    {
        using var memoryStream = new MemoryStream(request.FileContent.ToByteArray());

        var result = ParseMagneticFieldsFromStream(memoryStream, request.StartPoint, request.EndPoint, request.Step);

        return new OutputResponse
        {
            MagneticFieldsPoints = { result.z },
            MagneticFieldsValues = { result.bm },
            MagneticFieldMaxValues = result.bnorm
        };
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
            // логируй ошибку
            return null;
        }
    }
}

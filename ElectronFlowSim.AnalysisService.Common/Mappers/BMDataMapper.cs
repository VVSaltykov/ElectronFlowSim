using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;
using ElectronFlowSim.DTO.AnalysisService.OutputData;

namespace ElectronFlowSim.AnalysisService.Common.Mappers;
public class BmDataMapper : IBaseMapper<BMTableData, BMDataDTO, BmDataMapper>
{
    public static BMTableData ToEntity(BMDataDTO dto)
    {
        var entity = new BMTableData
        {
            Z = dto.z,
            Bm = dto.bm,
            bnorm = dto.bnorm,
        };
        
        return entity;
    }

    public static BMDataDTO ToDto(BMTableData entity)
    {
        var dto = new BMDataDTO
        {
            z = entity.Z,
            bm = entity.Bm,
            bnorm = entity.bnorm,
        };
        
        return dto;
    }
}
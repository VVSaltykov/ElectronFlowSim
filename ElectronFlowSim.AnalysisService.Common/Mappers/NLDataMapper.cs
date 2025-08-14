using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.AnalysisService.Common.Mappers;

public class NlDataMapper : IBaseMapper<NLTableData, NLTableDTO, NlDataMapper>
{
    public static NLTableData ToEntity(NLTableDTO dto)
    {
        var entity = new NLTableData
        {
            N = dto.N,
            L = dto.L,
        };
        
        return entity;
    }

    public static NLTableDTO ToDto(NLTableData entity)
    {
        var dto = new NLTableDTO
        {
            N = entity.N,
            L = entity.L,
        };
        
        return dto;
    }
}
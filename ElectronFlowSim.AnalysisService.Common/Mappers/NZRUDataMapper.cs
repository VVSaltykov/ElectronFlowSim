using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;
using ElectronFlowSim.DTO.AnalysisService.Enum;

namespace ElectronFlowSim.AnalysisService.Common.Mappers;

public class NzruDataMapper : IBaseMapper<NZRUTableData, NZRUTableDTO, NzruDataMapper>
{
    public static NZRUTableData ToEntity(NZRUTableDTO dto)
    {
        var entity = new NZRUTableData
        {
            N = dto.N,
            R = dto.R,
            Z = dto.Z,
            U = dto.U,
            WorkpieceType = (Domain.Enum.WorkpieceType)dto.WorkpieceType,
        };
        
        return entity;
    }

    public static NZRUTableDTO ToDto(NZRUTableData entity)
    {
        var dto = new NZRUTableDTO
        {
            N = entity.N,
            Z = entity.Z,
            R = entity.R,
            U = entity.U,
            WorkpieceType = (WorkpieceType)entity.WorkpieceType,
        };
        
        return dto;
    }
}
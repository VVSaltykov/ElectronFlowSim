using ElectronFlowSim.AnalysisService.Common.Extensions;
using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.AnalysisService.Common.Mappers;

public class InputDataForSaveMapper : IBaseMapper<InputData, InputDataForSaveDTO, InputDataForSaveMapper>
{
    public static InputData ToEntity(InputDataForSaveDTO dto)
    {
        return new InputData
        {
            ig = dto.ig,
            nmas = dto.nmas,
            km = dto.km,
            kp = dto.kp,
            kq = dto.kq,
            kpj6 = dto.kpj6,
            ik = dto.ik,
            nl = 0,
            j1 = dto.j1,
            icr = dto.icr,
            jcr = dto.jcr,
            
            NZRUTableDatas = dto.NZRUTableDatas?
                .Select(x => x.MapToEntity<NZRUTableData, NZRUTableDTO, NzruDataMapper>())
                .ToList(),
            
            NLTableData = dto.NLTableData?
                .MapToEntity<NLTableData, NLTableDTO, NlDataMapper>(),
            
            rk = dto.rk,
            utep = dto.utep,
            zkon = dto.zkon,
            akl1 = dto.akl1,
            u0 = dto.u0,
            uekvip = dto.uekvip,
            abm = dto.abm,
            
            BMTableData = dto.BMTableData?
                .MapToEntity<BMTableData, BMDataDTO, BmDataMapper>(),
            
            aik = dto.aik,
            ht = dto.ht,
            dz = dto.dz,
            dtok = dto.dtok,
            hq1 = dto.hq1,
            ar1s = dto.ar1s,
            SaveDateTime = DateTime.Now,
            SaveName = dto.SaveName,
        };
    }

    public static InputDataForSaveDTO ToDto(InputData entity)
    {
        return new InputDataForSaveDTO
        {
            ig = entity.ig,
            nmas = entity.nmas,
            km = entity.km,
            kp = entity.kp,
            kq = entity.kq,
            kpj6 = entity.kpj6,
            ik = entity.ik,
            nl = entity.nl,
            j1 = entity.j1,
            icr = entity.icr,
            jcr = entity.jcr,

            NZRUTableDatas = entity.NZRUTableDatas?
                .Select(x => x.MapToDto<NZRUTableData, NZRUTableDTO, NzruDataMapper>())
                .ToList(),

            NLTableData = entity.NLTableData?
                .MapToDto<NLTableData, NLTableDTO, NlDataMapper>(),

            BMTableData = entity.BMTableData?
                .MapToDto<BMTableData, BMDataDTO, BmDataMapper>(),

            rk = entity.rk,
            utep = entity.utep,
            zkon = entity.zkon,
            akl1 = entity.akl1,
            u0 = entity.u0,
            uekvip = entity.uekvip,
            abm = entity.abm,
            aik = entity.aik,
            ht = entity.ht,
            dz = entity.dz,
            dtok = entity.dtok,
            hq1 = entity.hq1,
            ar1s = entity.ar1s,
            SaveName = entity.SaveName
        };
    }
}
using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.AnalysisService.Common.Mappers;

public class InputDataMapper : IBaseMapper<InputData, InputDataDTO, InputDataMapper>
{
    public static InputData ToEntity(InputDataDTO dto)
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
            NLTableData = new NLTableData
            {
                L = dto.l.ToList(),
            },
            rk = dto.rk,
            utep = dto.utep,
            zkon = dto.zkon,
            akl1 = dto.akl1,
            u0 = dto.u0,
            uekvip = dto.uekvip,
            abm = dto.abm,
            BMTableData = new BMTableData
            {
                Z = dto.z.ToList(),
                Bm = dto.bm.ToList(),
                bnorm = dto.bnorm,
            },
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

    public static InputDataDTO ToDto(InputData entity)
    {
        throw new NotImplementedException();
    }
}
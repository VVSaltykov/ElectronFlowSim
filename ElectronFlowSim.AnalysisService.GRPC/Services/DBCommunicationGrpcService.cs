using ElectronFlowSim.AnalysisService.Common.Repositories;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.AnalysisService.GRPC.Protos;
using ElectronFlowSim.DTO.AnalysisService;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ElectronFlowSim.AnalysisService.GRPC.Services
{
    public class DBCommunicationGrpcService : DBCommunication.DBCommunicationBase
    {
        private readonly InputDataRepository inputDataRepository;

        public DBCommunicationGrpcService(InputDataRepository inputDataRepository)
        {
            this.inputDataRepository = inputDataRepository;
        }

        public override async Task<EmptyResponse> CreateSave(Protos.InputDataDTO _inputDataDTO, ServerCallContext context)
        {
            var inputDataDTO = new DTO.AnalysisService.InputDataDTO
            {
                ig = _inputDataDTO.Ig,
                nmas = _inputDataDTO.Nmas,
                km = _inputDataDTO.Km,
                kp = _inputDataDTO.Kp,
                kq = _inputDataDTO.Kq,
                kpj6 = _inputDataDTO.Kpj6,
                ik = _inputDataDTO.Ik,
                j1 = _inputDataDTO.J1,
                icr = _inputDataDTO.Icr,
                jcr = _inputDataDTO.Jcr,
                r = _inputDataDTO.R.ToArray(),
                z = _inputDataDTO.Z.ToArray(),
                u = _inputDataDTO.U.ToArray(),
                l = _inputDataDTO.L.ToArray(),
                rk = _inputDataDTO.Rk,
                utep = _inputDataDTO.Utep,
                zkon = _inputDataDTO.Zkon,
                akl1 = _inputDataDTO.Akl1,
                u0 = _inputDataDTO.U0,
                uekvip = _inputDataDTO.Uekvip.ToArray(),
                bnorm = _inputDataDTO.Bnorm,
                abm = _inputDataDTO.Abm,
                bm = _inputDataDTO.Bm.ToArray(),
                aik = _inputDataDTO.Aik.ToArray(),
                ht = _inputDataDTO.Ht,
                dz = _inputDataDTO.Dz,
                dtok = _inputDataDTO.Dtok,
                hq1 = _inputDataDTO.Hq1,
                ar1s = _inputDataDTO.Ar1S
            };

            await inputDataRepository.Create(inputDataDTO);

            return new EmptyResponse();
        }

        public override async Task<Protos.InputDataDTO> GetSave(EmptyResponse emptyResponse, ServerCallContext context)
        {
            var maxDate = await inputDataRepository.GetMaxSaveDateTime();
            var inputDataDTO = await inputDataRepository.Read(x => x.SaveDateTime == maxDate);

            var _inputDataDTO = new Protos.InputDataDTO
            {
                Ig = inputDataDTO.ig,
                Nmas = inputDataDTO.nmas,
                Km = inputDataDTO.km,
                Kp = inputDataDTO.kp,
                Kq = inputDataDTO.kq,
                Kpj6 = inputDataDTO.kpj6,
                Ik = inputDataDTO.ik,
                J1 = inputDataDTO.j1,
                Icr = inputDataDTO.icr,
                Jcr = inputDataDTO.jcr,
                Rk = inputDataDTO.rk,
                Utep = inputDataDTO.utep,
                Zkon = inputDataDTO.zkon,
                Akl1 = inputDataDTO.akl1,
                U0 = inputDataDTO.u0,
                Bnorm = inputDataDTO.bnorm,
                Abm = inputDataDTO.abm,
                Ht = inputDataDTO.ht,
                Dz = inputDataDTO.dz,
                Dtok = inputDataDTO.dtok,
                Hq1 = inputDataDTO.hq1,
                Ar1S = inputDataDTO.ar1s
            };

            _inputDataDTO.R.AddRange(inputDataDTO.r);
            _inputDataDTO.Z.AddRange(inputDataDTO.z);
            _inputDataDTO.U.AddRange(inputDataDTO.u);
            _inputDataDTO.L.AddRange(inputDataDTO.l);
            _inputDataDTO.Uekvip.AddRange(inputDataDTO.uekvip);
            _inputDataDTO.Bm.AddRange(inputDataDTO.bm);
            _inputDataDTO.Aik.AddRange(inputDataDTO.aik);

            return _inputDataDTO;
        }
    }
}

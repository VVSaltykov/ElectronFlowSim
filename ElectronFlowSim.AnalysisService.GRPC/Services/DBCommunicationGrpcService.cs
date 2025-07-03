using ElectronFlowSim.AnalysisService.Common.Repositories;
using ElectronFlowSim.AnalysisService.GRPC.Protos;
using Grpc.Core;

namespace ElectronFlowSim.AnalysisService.GRPC.Services
{
    public class DBCommunicationGrpcService : DBCommunication.DBCommunicationBase
    {
        private readonly InputDataRepository inputDataRepository;

        public DBCommunicationGrpcService(InputDataRepository inputDataRepository)
        {
            this.inputDataRepository = inputDataRepository;
        }

        public override async Task<EmptyResponse> CreateSave(InputDataDTO _inputDataDTO, ServerCallContext context)
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
    }
}

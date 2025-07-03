using ElectronFlowSim.AnalysisService.GRPC.Protos;
using ElectronFlowSim.DTO.AnalysisService;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using InputDataDTO = ElectronFlowSim.DTO.AnalysisService.InputDataDTO;

namespace ElectronFlowSim.API.Controllers
{
    [ApiController]
    [Route("api/data/")]
    public class DataController : ControllerBase
    {
        private readonly DBCommunication.DBCommunicationClient dBCommunicationClient;

        public DataController(DBCommunication.DBCommunicationClient dBCommunicationClient)
        {
            this.dBCommunicationClient = dBCommunicationClient;
        }

        [HttpPost("save-data")]
        public async Task<IActionResult> CreateSave([FromBody] InputDataDTO _inputDataDTO)
        {
            var inputDataDTO = new AnalysisService.GRPC.Protos.InputDataDTO
            {
                Ig = _inputDataDTO.ig,
                Nmas = _inputDataDTO.nmas,
                Km = _inputDataDTO.km,
                Kp = _inputDataDTO.kp,
                Kq = _inputDataDTO.kq,
                Kpj6 = _inputDataDTO.kpj6,
                Ik = _inputDataDTO.ik,
                J1 = _inputDataDTO.j1,
                Icr = _inputDataDTO.icr,
                Jcr = _inputDataDTO.jcr,
                Rk = _inputDataDTO.rk,
                Utep = _inputDataDTO.utep,
                Zkon = _inputDataDTO.zkon,
                Akl1 = _inputDataDTO.akl1,
                U0 = _inputDataDTO.u0,
                Bnorm = _inputDataDTO.bnorm,
                Abm = _inputDataDTO.abm,
                Ht = _inputDataDTO.ht,
                Dz = _inputDataDTO.dz,
                Dtok = _inputDataDTO.dtok,
                Hq1 = _inputDataDTO.hq1,
                Ar1S = _inputDataDTO.ar1s
            };

            inputDataDTO.R.AddRange(_inputDataDTO.r);
            inputDataDTO.Z.AddRange(_inputDataDTO.z);
            inputDataDTO.U.AddRange(_inputDataDTO.u);
            inputDataDTO.L.AddRange(_inputDataDTO.l);
            inputDataDTO.Uekvip.AddRange(_inputDataDTO.uekvip);
            inputDataDTO.Bm.AddRange(_inputDataDTO.bm);
            inputDataDTO.Aik.AddRange(_inputDataDTO.aik);

            var grpcResponse = await dBCommunicationClient.CreateSaveAsync(inputDataDTO);

            return Ok(grpcResponse);
        }

        [HttpGet("get-save")]
        public async Task<IActionResult> GetData()
        {
            var result = await dBCommunicationClient.GetSaveAsync(new EmptyResponse());
            return Ok(result);
        }
    }
}

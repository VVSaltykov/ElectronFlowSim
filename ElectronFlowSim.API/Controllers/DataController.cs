using ElectronFlowSim.AnalysisService.GRPC.Protos;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

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

        [HttpPost("get-drawing-result")]
        public async Task<IActionResult> CreateSave([FromBody] InputDataDTO inputDataDTO)
        {
            var grpcResponse = await dBCommunicationClient.CreateSaveAsync(inputDataDTO);

            return Ok(grpcResponse);
        }
    }
}

using ElectronFlowSim.AnalysisService.GRPC.Protos;
using ElectronFlowSim.DTO.AnalysisService;
using Google.Protobuf.WellKnownTypes;
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

        /// <summary>
        /// Сохранение входных данных
        /// </summary>
        /// <param name="_inputDataDTO"></param>
        /// <param name="saveName"></param>
        /// <returns></returns>
        [HttpPost("save-data")]
        public async Task<IActionResult> CreateSave([FromBody] InputDataForSaveDTO _inputDataDTO, string saveName)
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
                Abm = _inputDataDTO.abm,
                Ht = _inputDataDTO.ht,
                Dz = _inputDataDTO.dz,
                Dtok = _inputDataDTO.dtok,
                Hq1 = _inputDataDTO.hq1,
                Ar1S = _inputDataDTO.ar1s,
                SaveName = saveName,
                Nl = _inputDataDTO.nl,
            };


            inputDataDTO.Uekvip.AddRange(_inputDataDTO.uekvip);
            inputDataDTO.Aik.AddRange(_inputDataDTO.aik);

            if (_inputDataDTO.NZRUTableDatas != null && _inputDataDTO.NZRUTableDatas.Any())
            {
                var nzruData = new NZRUTableData();
                foreach (var tableDto in _inputDataDTO.NZRUTableDatas)
                {
                    var electrode = new ElectrodeData
                    {
                        U = tableDto.U,
                        WorkpieceType = tableDto.WorkpieceType.ToString()
                    };

                    electrode.N.AddRange(tableDto.N ?? new List<int>());
                    electrode.Z.AddRange(tableDto.Z ?? new List<double>());
                    electrode.R.AddRange(tableDto.R ?? new List<double>());

                    nzruData.Electrodes.Add(electrode);
                }
                inputDataDTO.NzruData = nzruData;
            }

            if (_inputDataDTO.NLTableData != null)
            {
                var nlTableData = new NLTableData();
                nlTableData.N.AddRange(_inputDataDTO.NLTableData.N ?? new List<int>());
                nlTableData.L.AddRange(_inputDataDTO.NLTableData.L ?? new List<int>());
                inputDataDTO.NlTableData = nlTableData;
            }

            if (_inputDataDTO.BMTableData != null)
            {
                var bmTableData = new BMTableData();
                bmTableData.Z.AddRange(_inputDataDTO.BMTableData.z ?? new List<double>());
                bmTableData.Bm.AddRange(_inputDataDTO.BMTableData.bm ?? new List<double>());
                bmTableData.Bnorm = _inputDataDTO.BMTableData.bnorm;
                inputDataDTO.BmTableData = bmTableData;
            }

            var grpcResponse = await dBCommunicationClient.CreateSaveAsync(inputDataDTO);

            return Ok(new
            {
                Request = grpcResponse,
                Message = "Данные сохранены."
            });
        }

        /// <summary>
        /// Получение последнего сохранения
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-last-save")]
        public async Task<IActionResult> GetLastSave()
        {
            var result = await dBCommunicationClient.GetLastSaveAsync(new EmptyRequest());
            return Ok(new
            {
                Request = result,
                Message = "Загружено последнее сохранение"
            });
        }

        /// <summary>
        /// Получение названий сохранений для выбора
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-save-names")]
        public async Task<IActionResult> GetSaveNames()
        {
            var result = await dBCommunicationClient.GetSaveNamesAsync(new EmptyRequest());
            // Преобразуем protobuf Timestamp в удобочитаемый формат
            var response = result.Items.Select(item => new
            {
                Name = item.Name,
                SaveDateTime = item.SaveDateTime.ToDateTime() // Конвертирует Timestamp в DateTime
            }).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Получение определенного сохранения
        /// </summary>
        /// <param name="saveName"></param>
        /// <param name="saveDateTime"></param>
        /// <returns></returns>
        [HttpGet("get-save")]
        public async Task<IActionResult> GetSave([FromQuery] string saveName, DateTime saveDateTime)
        {
            var getSaveRequest = new AnalysisService.GRPC.Protos.GetSaveRequest
            {
                SaveName = saveName,
                SaveDateTime = saveDateTime.ToTimestamp()
            };

            var result = await dBCommunicationClient.GetSaveAsync(getSaveRequest);
            return Ok(new
            {
                Request = result,
                Message = $"Загружено сохранение {result.SaveName} от {result.SaveDateTime}"
            });
        }

        [HttpDelete("delete-save")]
        public async Task<IActionResult> DeleteSave([FromQuery] string saveName, DateTime saveDateTime)
        {
            var deleteSaveRequest = new AnalysisService.GRPC.Protos.DeleteSaveRequest
            {
                SaveName = saveName,
                SaveDateTime = saveDateTime.ToTimestamp()
            };

            await dBCommunicationClient.DeleteSaveAsync(deleteSaveRequest);

            return Ok(new
            {
                Message = $"Сохранение удалено"
            });
        }
    }
}

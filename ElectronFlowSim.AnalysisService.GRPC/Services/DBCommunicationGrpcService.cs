using ElectronFlowSim.AnalysisService.Common.Repositories;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.AnalysisService.GRPC.Protos;
using ElectronFlowSim.DTO.AnalysisService;
using ElectronFlowSim.DTO.AnalysisService.Enum;
using Google.Protobuf.WellKnownTypes;
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

        /// <summary>
        /// Создание сохранения входных данных
        /// </summary>
        /// <param name="_inputDataDTO"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<EmptyResponse> CreateSave(Protos.InputDataDTO _inputDataDTO, ServerCallContext context)
        {
            var inputDataDTO = new DTO.AnalysisService.InputDataForSaveDTO
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
                rk = _inputDataDTO.Rk,
                utep = _inputDataDTO.Utep,
                zkon = _inputDataDTO.Zkon,
                akl1 = _inputDataDTO.Akl1,
                u0 = _inputDataDTO.U0,
                uekvip = _inputDataDTO.Uekvip.ToArray(),
                abm = _inputDataDTO.Abm,
                aik = _inputDataDTO.Aik.ToArray(),
                ht = _inputDataDTO.Ht,
                dz = _inputDataDTO.Dz,
                dtok = _inputDataDTO.Dtok,
                hq1 = _inputDataDTO.Hq1,
                ar1s = _inputDataDTO.Ar1S,
                SaveName = _inputDataDTO.SaveName,
                nl = _inputDataDTO.Nl,
            };

            if (_inputDataDTO.NzruData != null)
            {
                inputDataDTO.NZRUTableDatas = new List<NZRUTableDTO>();
                foreach (var electrode in _inputDataDTO.NzruData.Electrodes)
                {
                    var tableDto = new NZRUTableDTO
                    {
                        U = electrode.U,
                        WorkpieceType = System.Enum.Parse<WorkpieceType>(electrode.WorkpieceType),
                        N = electrode.N.ToList(),
                        Z = electrode.Z.ToList(),
                        R = electrode.R.ToList()
                    };
                    inputDataDTO.NZRUTableDatas.Add(tableDto);
                }
            }

            if (_inputDataDTO.NlTableData != null)
            {
                inputDataDTO.NLTableData = new NLTableDTO
                {
                    N = _inputDataDTO.NlTableData.N.ToList(),
                    L = _inputDataDTO.NlTableData.L.ToList()
                };
            }

            if (_inputDataDTO.BmTableData != null)
            {
                inputDataDTO.BMTableData = new BMDataDTO
                {
                    z = _inputDataDTO.BmTableData.Z.ToList(),
                    bm = _inputDataDTO.BmTableData.Bm.ToList(),
                    bnorm = _inputDataDTO.BmTableData.Bnorm
                };
            }

            await inputDataRepository.Create(inputDataDTO);

            return new EmptyResponse();
        }

        /// <summary>
        /// Получение последнего сохранения
        /// </summary>
        /// <param name="emptyRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<Protos.InputDataDTO> GetLastSave(EmptyRequest emptyRequest, ServerCallContext context)
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
                Abm = inputDataDTO.abm,
                Ht = inputDataDTO.ht,
                Dz = inputDataDTO.dz,
                Dtok = inputDataDTO.dtok,
                Hq1 = inputDataDTO.hq1,
                Ar1S = inputDataDTO.ar1s
            };

            _inputDataDTO.Uekvip.AddRange(inputDataDTO.uekvip);
            _inputDataDTO.Aik.AddRange(inputDataDTO.aik);

            if (inputDataDTO.NZRUTableDatas != null && inputDataDTO.NZRUTableDatas.Any())
            {
                var nzruData = new Protos.NZRUTableData();
                foreach (var tableDto in inputDataDTO.NZRUTableDatas)
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
                _inputDataDTO.NzruData = nzruData;
            }

            if (inputDataDTO.NLTableData != null)
            {
                var nlTableData = new Protos.NLTableData();
                nlTableData.N.AddRange(inputDataDTO.NLTableData.N ?? new List<int>());
                nlTableData.L.AddRange(inputDataDTO.NLTableData.L ?? new List<int>());
                _inputDataDTO.NlTableData = nlTableData;
            }

            if (_inputDataDTO.BmTableData != null)
            {
                var bmTableData = new Protos.BMTableData();
                bmTableData.Z.AddRange(inputDataDTO.BMTableData.Z ?? new List<double>());
                bmTableData.Bm.AddRange(inputDataDTO.BMTableData.Bm ?? new List<double>());
                bmTableData.Bnorm = inputDataDTO.BMTableData.bnorm;
                _inputDataDTO.BmTableData = bmTableData;
            }

            return _inputDataDTO;
        }

        /// <summary>
        /// Получение всех созданных сохранений
        /// </summary>
        /// <param name="emptyRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<SaveNames> GetSaveNames(EmptyRequest emptyRequest, ServerCallContext context)
        {
            var saveData = await inputDataRepository.GetSaveNames();

            var response = new SaveNames();

            if (saveData != null)
            {
                foreach (var item in saveData)
                {
                    var saveInfo = new SaveInfo
                    {
                        Name = item.SaveName,
                        SaveDateTime = item.SaveDate.HasValue
                            ? Timestamp.FromDateTime(item.SaveDate.Value.ToUniversalTime())
                            : null
                    };
                    response.Items.Add(saveInfo);
                }
            }

            return response;
        }

        /// <summary>
        /// Получение определенного сохранения
        /// </summary>
        /// <param name="getSaveRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<SaveData> GetSave(GetSaveRequest getSaveRequest, ServerCallContext context)
        {
            var result = await inputDataRepository.GetSaveData(getSaveRequest.SaveName, getSaveRequest.SaveDateTime.ToDateTime());

            var data = new SaveData
            {
                Ig = result.ig,
                Nmas = result.nmas,
                Km = result.km,
                Kp = result.kp,
                Kq = result.kq,
                Kpj6 = result.kpj6,
                Ik = result.ik,
                J1 = result.j1,
                Icr = result.icr,
                Jcr = result.jcr,
                Rk = result.rk,
                Utep = result.utep,
                Zkon = result.zkon,
                Akl1 = result.akl1,
                U0 = result.u0,
                Abm = result.abm,
                Ht = result.ht,
                Dz = result.dz,
                Dtok = result.dtok,
                Hq1 = result.hq1,
                Ar1S = result.ar1s
            };

            data.Uekvip.AddRange(result.uekvip);
            data.Aik.AddRange(result.aik);

            if (result.NZRUTableDatas != null && result.NZRUTableDatas.Any())
            {
                var nzruData = new Protos.NZRUTableData();
                foreach (var tableDto in result.NZRUTableDatas)
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
                data.NzruData = nzruData;
            }

            if (result.NLTableData != null)
            {
                var nlTableData = new Protos.NLTableData();
                nlTableData.N.AddRange(result.NLTableData.N ?? new List<int>());
                nlTableData.L.AddRange(result.NLTableData.L ?? new List<int>());
                data.NlTableData = nlTableData;
            }

            if (result.BMTableData != null)
            {
                var bmTableData = new Protos.BMTableData();
                bmTableData.Z.AddRange(result.BMTableData.Z ?? new List<double>());
                bmTableData.Bm.AddRange(result.BMTableData.Bm ?? new List<double>());
                bmTableData.Bnorm = result.BMTableData.bnorm;
                data.BmTableData = bmTableData;
            }

            return data;
        }

        public override async Task<EmptyResponse> DeleteSave(DeleteSaveRequest request, ServerCallContext context)
        {
            DateTime saveDateTime = request.SaveDateTime.ToDateTime();

            var save = await inputDataRepository.Read(predicate: x => x.SaveName == request.SaveName
                && x.SaveDateTime == saveDateTime);

            await inputDataRepository.Delete(save.Id);

            return new EmptyResponse();
        }
    }
}

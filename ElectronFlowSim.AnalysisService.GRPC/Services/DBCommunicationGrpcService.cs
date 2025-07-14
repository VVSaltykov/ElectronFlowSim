using ElectronFlowSim.AnalysisService.Common.Repositories;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.AnalysisService.GRPC.Protos;
using ElectronFlowSim.DTO.AnalysisService;
using ElectronFlowSim.DTO.AnalysisService.Enum;
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
                bnorm = _inputDataDTO.Bnorm,
                abm = _inputDataDTO.Abm,
                bm = _inputDataDTO.Bm.ToArray(),
                aik = _inputDataDTO.Aik.ToArray(),
                ht = _inputDataDTO.Ht,
                dz = _inputDataDTO.Dz,
                dtok = _inputDataDTO.Dtok,
                hq1 = _inputDataDTO.Hq1,
                ar1s = _inputDataDTO.Ar1S,
                SaveName = _inputDataDTO.SaveName
            };

            if (_inputDataDTO.NzruData != null)
            {
                inputDataDTO.NZRUTableDatas = new List<NZRUTableDTO>();
                foreach (var electrode in _inputDataDTO.NzruData.Electrodes)
                {
                    var tableDto = new NZRUTableDTO
                    {
                        U = electrode.U,
                        WorkpieceType = Enum.Parse<WorkpieceType>(electrode.WorkpieceType),
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

            await inputDataRepository.Create(inputDataDTO);

            return new EmptyResponse();
        }

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
                Bnorm = inputDataDTO.bnorm,
                Abm = inputDataDTO.abm,
                Ht = inputDataDTO.ht,
                Dz = inputDataDTO.dz,
                Dtok = inputDataDTO.dtok,
                Hq1 = inputDataDTO.hq1,
                Ar1S = inputDataDTO.ar1s
            };

            _inputDataDTO.Uekvip.AddRange(inputDataDTO.uekvip);
            _inputDataDTO.Bm.AddRange(inputDataDTO.bm);
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

            return _inputDataDTO;
        }

        public override async Task<SaveNames> GetSaveNames(EmptyRequest emptyRequest, ServerCallContext context)
        {
            var saveNames = await inputDataRepository.GetSaveNames();

            var data = new SaveNames();

            data.Names.AddRange(saveNames);

            return data;
        }

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
                Bnorm = result.bnorm,
                Abm = result.abm,
                Ht = result.ht,
                Dz = result.dz,
                Dtok = result.dtok,
                Hq1 = result.hq1,
                Ar1S = result.ar1s
            };

            data.Uekvip.AddRange(result.uekvip);
            data.Bm.AddRange(result.bm);
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

            return data;
        }
    }
}

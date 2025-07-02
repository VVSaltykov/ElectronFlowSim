using AutoMapper;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.AnalysisService.Common.Mappers
{
    public class InputDataMapperConfiguration : Profile
    {
        public InputDataMapperConfiguration()
        {
            CreateMap<InputDataDTO, InputData>();

            CreateMap<InputData, InputDataDTO>();
        }
    }
}

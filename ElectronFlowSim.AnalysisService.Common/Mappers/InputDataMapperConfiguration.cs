using AutoMapper;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.AnalysisService.Common.Mappers
{
    /// <summary>
    /// Маппер сохранения входных данных 
    /// </summary>
    public class InputDataMapperConfiguration : Profile //TODO:разнести по разным конфигурациям
    {
        public InputDataMapperConfiguration()
        {
            CreateMap<InputDataDTO, InputData>();

            CreateMap<InputData, InputDataDTO>();

            CreateMap<NZRUTableDTO, NZRUTableData>() 
                .ForMember(dest => dest.N, opt => opt.MapFrom(src => src.N))
                .ForMember(dest => dest.Z, opt => opt.MapFrom(src => src.Z))
                .ForMember(dest => dest.R, opt => opt.MapFrom(src => src.R))
                .ForMember(dest => dest.U, opt => opt.MapFrom(src => src.U))
                .ForMember(dest => dest.WorkpieceType, opt => opt.MapFrom(src => src.WorkpieceType));

            CreateMap<NLTableDTO, NLTableData>()
                .ForMember(dest => dest.N, opt => opt.MapFrom(src => src.N))
                .ForMember(dest => dest.L, opt => opt.MapFrom(src => src.L));

            CreateMap<BMDataDTO, BMTableData>()
                .ForMember(dest => dest.Z, opt => opt.MapFrom(src => src.z))
                .ForMember(dest => dest.Bm, opt => opt.MapFrom(src => src.bm))
                .ForMember(dest => dest.bnorm, opt => opt.MapFrom(src => src.bnorm));

            CreateMap<InputDataForSaveDTO, InputData>()
                .ForMember(dest => dest.NZRUTableDatas, opt => opt.MapFrom(src => src.NZRUTableDatas))
                .ForMember(dest => dest.NLTableData, opt => opt.MapFrom(src => src.NLTableData));

            CreateMap<InputData, InputDataForSaveDTO>();
        }
    }
}

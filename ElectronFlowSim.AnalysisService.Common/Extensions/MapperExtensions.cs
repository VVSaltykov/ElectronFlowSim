using ElectronFlowSim.AnalysisService.Common.Interfaces;

namespace ElectronFlowSim.AnalysisService.Common.Extensions;

public static class MapperExtensions
{
    public static TEntity MapToEntity<TEntity, TDto, TMapper>(this TDto dto)
        where TMapper : IBaseMapper<TEntity, TDto, TMapper>
    {
        return TMapper.ToEntity(dto);
    }

    public static TDto MapToDto<TEntity, TDto, TMapper>(this TEntity entity)
        where TMapper : IBaseMapper<TEntity, TDto, TMapper>
    {
        return TMapper.ToDto(entity);
    }
}
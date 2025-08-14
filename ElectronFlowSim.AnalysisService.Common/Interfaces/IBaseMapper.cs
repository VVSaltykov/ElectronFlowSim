namespace ElectronFlowSim.AnalysisService.Common.Interfaces;

public interface IBaseMapper<TEntity, TDto, TSelf>
    where TSelf : IBaseMapper<TEntity, TDto, TSelf>
{
    static abstract TEntity ToEntity(TDto dto);
    static abstract TDto ToDto(TEntity entity);
}
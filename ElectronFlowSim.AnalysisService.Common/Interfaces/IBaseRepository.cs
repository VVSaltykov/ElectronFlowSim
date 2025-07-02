using ElectronFlowSim.AnalysisService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.AnalysisService.Common.Interfaces
{
    public interface IBaseRepository<TEntity, TEntityDTO> 
        where TEntity : class
        where TEntityDTO : class
    {
        Task Create(TEntityDTO entity);
        Task<TEntity?> Read(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
        Task<List<TEntity>?> ReadMany(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
        Task Update(TEntityDTO entity);
        Task Delete(Guid entityId);
    }
}

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

    /// <summary>
    /// Базовый CRUD репозиторий
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDTO"></typeparam>
    public interface IBaseRepository<TEntity, TEntityDTO> 
        where TEntity : class
        where TEntityDTO : class
    {
        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(TEntityDTO entity);

        /// <summary>
        /// Чтение сущности с определенным условием и возможность добавления вложенных сущностей
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        Task<TEntity?> Read(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        /// <summary>
        /// Чтение сущностей с определенным условием и возможность добавления вложенных сущностей
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        Task<List<TEntity>?> ReadMany(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Update(TEntityDTO dto);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task Delete(Guid entityId);
    }
}

using Calabonga.UnitOfWork;
using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using ElectronFlowSim.AnalysisService.Common.Extensions;
using ElectronFlowSim.AnalysisService.Common.Mappers;

namespace ElectronFlowSim.AnalysisService.Common.Repositories
{
    public class InputDataRepository : IBaseRepository<InputData, InputDataForSaveDTO> 
    {
        private readonly IUnitOfWork unitOfWork;

        public InputDataRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Create(InputDataForSaveDTO dto)
        {
            try
            {
                var inputData = dto.MapToEntity<InputData, InputDataForSaveDTO, InputDataForSaveMapper>();

                inputData.SaveDateTime = DateTime.UtcNow;

                var inputDataRepository = unitOfWork.GetRepository<InputData>();

                await inputDataRepository.InsertAsync(inputData);

                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(Guid entityId)
        {
            try
            {
                var repository = unitOfWork.GetRepository<InputData>();

                var entity = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == entityId);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Сущность с ID {entityId} не найдена");
                }

                repository.Delete(entity);

                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<InputData?> Read(Expression<Func<InputData, bool>> predicate,
            Func<IQueryable<InputData>, IIncludableQueryable<InputData, object>>? include = null)
        {
            var repository = unitOfWork.GetRepository<InputData>();
            return await repository.GetFirstOrDefaultAsync(predicate: predicate, include: include);
        }

        public async Task<List<InputData>?> ReadMany(Expression<Func<InputData, bool>> predicate,
            Func<IQueryable<InputData>, IIncludableQueryable<InputData, object>>? include = null)
        {
            var repository = unitOfWork.GetRepository<InputData>();
            var result = await repository.GetAllAsync(predicate: predicate, include: include);
            return result.ToList();
        }

        public async Task Update(InputDataForSaveDTO dto)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();

                var inputData = dto.MapToEntity<InputData, InputDataForSaveDTO, InputDataForSaveMapper>();

                var repository = unitOfWork.GetRepository<InputData>();

                repository.Update(inputData);

                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Получение последнего времени сохранения
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime?> GetMaxSaveDateTime()
        {
            var repository = unitOfWork.GetRepository<InputData>();
            return await repository.MaxAsync(x => x.SaveDateTime);
        }

        /// <summary>
        /// Получение имен сохранений
        /// </summary>
        /// <returns></returns>
        public async Task<List<(string SaveName, DateTime? SaveDate)>?> GetSaveNames()
        {
            var repository = unitOfWork.GetRepository<InputData>();
            var data = repository.GetAll(trackingType: TrackingType.NoTracking).ToList();

            return data.Select(x => (x.SaveName, x.SaveDateTime)).ToList();
        }


        /// <summary>
        /// Получение сохранения
        /// </summary>
        /// <param name="saveName"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<InputData> GetSaveData(string saveName, DateTime dateTime)
        {
            var repository = unitOfWork.GetRepository<InputData>();

            var data = await repository.GetFirstOrDefaultAsync(predicate: x => x.SaveName == saveName && x.SaveDateTime == dateTime.ToUniversalTime(),
                include: x => x.Include(x => x.NZRUTableDatas).Include(x => x.NLTableData).Include(x => x.BMTableData));

            return data;
        }
    }
}

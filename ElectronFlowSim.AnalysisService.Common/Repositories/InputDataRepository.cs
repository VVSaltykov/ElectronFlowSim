using AutoMapper;
using Calabonga.UnitOfWork;
using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq.Expressions;

namespace ElectronFlowSim.AnalysisService.Common.Repositories
{
    public class InputDataRepository : IBaseRepository<InputData, InputDataForSaveDTO> 
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InputDataRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Create(InputDataForSaveDTO entity)
        {
            try
            {
                var inputData = mapper.Map<InputData>(entity);

                var nlTableDataRepository = unitOfWork.GetRepository<NLTableData>();

                await nlTableDataRepository.InsertAsync(inputData.NLTableData);

                var nzruTableDataRepository = unitOfWork.GetRepository<NZRUTableData>();

                foreach (var nzruTableData in inputData.NZRUTableDatas)
                {
                    await nzruTableDataRepository.InsertAsync(nzruTableData);
                }

                var inputDataRepository = unitOfWork.GetRepository<InputData>();

                inputData.SaveDateTime = DateTime.UtcNow;

                await inputDataRepository.InsertAsync(inputData);

                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task Delete(Guid entityId)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();

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

        public async Task Update(InputDataForSaveDTO entity)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();

                var inputData = mapper.Map<InputData>(entity);

                var repository = unitOfWork.GetRepository<InputData>();

                repository.Update(inputData);

                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<DateTime?> GetMaxSaveDateTime()
        {
            var repository = unitOfWork.GetRepository<InputData>();
            return await repository.MaxAsync(x => x.SaveDateTime);
        }

        public async Task<List<(string SaveName, DateTime? SaveDate)>?> GetSaveNames()
        {
            var repository = unitOfWork.GetRepository<InputData>();
            var data = repository.GetAll(trackingType: TrackingType.NoTracking).ToList();

            return data.Select(x => (x.SaveName, x.SaveDateTime)).ToList();
        }

        public async Task<InputData> GetSaveData(string saveName, DateTime dateTime)
        {
            var repository = unitOfWork.GetRepository<InputData>();

            var data = await repository.GetFirstOrDefaultAsync(predicate: x => x.SaveName == saveName && x.SaveDateTime == dateTime.ToUniversalTime(),
                include: x => x.Include(x => x.NLTableData).Include(x => x.NLTableData));

            return data;
        }
    }
}

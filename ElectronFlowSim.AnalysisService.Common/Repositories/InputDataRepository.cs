using AutoMapper;
using Calabonga.UnitOfWork;
using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.DTO.AnalysisService;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ElectronFlowSim.AnalysisService.Common.Repositories
{
    public class InputDataRepository : IBaseRepository<InputData, InputDataDTO> 
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InputDataRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Create(InputDataDTO entity)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();

                var inputData = mapper.Map<InputData>(entity);

                var repository = unitOfWork.GetRepository<InputData>();

                await repository.InsertAsync(inputData);

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

        public async Task Update(InputDataDTO entity)
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
    }
}

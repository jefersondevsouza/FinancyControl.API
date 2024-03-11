using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Models.Queries;
using FinancyControl.API.Domain.Repositories;
using FinancyControl.API.Domain.Services;
using FinancyControl.API.Domain.Services.Communication;
using FinancyControl.API.Infraestructure;
using Microsoft.Extensions.Caching.Memory;

namespace FinancyControl.API.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IDespesaRepository _DespesaRepository;
        private readonly ITipoDespesaRepository _TipoDespesaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<DespesaService> _logger;

        public DespesaService
        (
            IDespesaRepository DespesaRepository,
            ITipoDespesaRepository TipoDespesaRepository,
            IUnitOfWork unitOfWork,
            IMemoryCache cache,
            ILogger<DespesaService> logger
        )
        {
            _DespesaRepository = DespesaRepository;
            _TipoDespesaRepository = TipoDespesaRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }

        public async Task<QueryResult<Despesa>> ListAsync(DespesaQuery query)
        {
            // Here I list the query result from cache if they exist, but now the data can vary according to the TipoDespesa ID, page and amount of
            // items per page. I have to compose a cache to avoid returning wrong data.
            string cacheKey = GetCacheKeyForDespesasQuery(query);

            var Despesas = await _cache.GetOrCreateAsync(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _DespesaRepository.ListAsync(query);
            });

            return Despesas!;
        }

        public async Task<Response<Despesa>> SaveAsync(Despesa Despesa)
        {
            try
            {
                /*
                 Notice here we have to check if the TipoDespesa ID is valid before adding the Despesa, to avoid errors.
                 You can create a method into the TipoDespesaService class to return the TipoDespesa and inject the service here if you prefer, but 
                 it doesn't matter given the API scope.
                */
                var existingTipoDespesa = await _TipoDespesaRepository.FindByIdAsync(Despesa.TipoDespesaId);
                if (existingTipoDespesa == null)
                    return new Response<Despesa>("Invalid TipoDespesa.");

                await _DespesaRepository.AddAsync(Despesa);
                await _unitOfWork.CompleteAsync();

                return new Response<Despesa>(Despesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not save Despesa.");
                return new Response<Despesa>($"An error occurred when saving the Despesa: {ex.Message}");
            }
        }

        public async Task<Response<Despesa>> UpdateAsync(int id, Despesa Despesa)
        {
            var existingDespesa = await _DespesaRepository.FindByIdAsync(id);

            if (existingDespesa == null)
                return new Response<Despesa>("Despesa not found.");

            var existingTipoDespesa = await _TipoDespesaRepository.FindByIdAsync(Despesa.TipoDespesaId);
            if (existingTipoDespesa == null)
                return new Response<Despesa>("Invalid TipoDespesa.");

            existingDespesa.Descricao = Despesa.Descricao;
            existingDespesa.Valor = Despesa.Valor;
            existingDespesa.TipoDespesaId = Despesa.TipoDespesaId;

            try
            {
                _DespesaRepository.Update(existingDespesa);
                await _unitOfWork.CompleteAsync();

                return new Response<Despesa>(existingDespesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not update Despesa with ID {id}.", id);
                return new Response<Despesa>($"An error occurred when updating the Despesa: {ex.Message}");
            }
        }

        public async Task<Response<Despesa>> DeleteAsync(int id)
        {
            var existingDespesa = await _DespesaRepository.FindByIdAsync(id);

            if (existingDespesa == null)
                return new Response<Despesa>("Despesa not found.");

            try
            {
                _DespesaRepository.Remove(existingDespesa);
                await _unitOfWork.CompleteAsync();

                return new Response<Despesa>(existingDespesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not delete Despesa with ID {id}.", id);
                return new Response<Despesa>($"An error occurred when deleting the Despesa: {ex.Message}");
            }
        }

        private static string GetCacheKeyForDespesasQuery(DespesaQuery query)
            => $"{CacheKeys.DespesasList}_{query.TipoDespesaId}_{query.Page}_{query.ItemsPerPage}";
    }
}

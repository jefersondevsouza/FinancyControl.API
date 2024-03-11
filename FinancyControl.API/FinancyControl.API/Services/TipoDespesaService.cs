using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Repositories;
using FinancyControl.API.Domain.Services;
using FinancyControl.API.Domain.Services.Communication;
using FinancyControl.API.Infraestructure;
using Microsoft.Extensions.Caching.Memory;

namespace FinancyControl.API.Services
{
    public class TipoDespesaService : ITipoDespesaService
    {
        private readonly ITipoDespesaRepository _TipoDespesaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<TipoDespesaService> _logger;

        public TipoDespesaService
        (
            ITipoDespesaRepository TipoDespesaRepository,
            IUnitOfWork unitOfWork,
            IMemoryCache cache,
            ILogger<TipoDespesaService> logger
        )
        {
            _TipoDespesaRepository = TipoDespesaRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<TipoDespesa>> ListAsync()
        {
            // Here I try to get the categories list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the categories from the repository.
            var categories = await _cache.GetOrCreateAsync(CacheKeys.TiposDespesaList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _TipoDespesaRepository.ListAsync();
            });

            return categories ?? new List<TipoDespesa>();
        }

        public async Task<Response<TipoDespesa>> SaveAsync(TipoDespesa TipoDespesa)
        {
            try
            {
                await _TipoDespesaRepository.AddAsync(TipoDespesa);
                await _unitOfWork.CompleteAsync();

                return new Response<TipoDespesa>(TipoDespesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not save TipoDespesa.");
                return new Response<TipoDespesa>($"An error occurred when saving the TipoDespesa: {ex.Message}");
            }
        }

        public async Task<Response<TipoDespesa>> UpdateAsync(int id, TipoDespesa TipoDespesa)
        {
            var existingTipoDespesa = await _TipoDespesaRepository.FindByIdAsync(id);
            if (existingTipoDespesa == null)
            {
                return new Response<TipoDespesa>("TipoDespesa not found.");
            }

            existingTipoDespesa.Name = TipoDespesa.Name;
            existingTipoDespesa.ClassificacaoDespesa = TipoDespesa.ClassificacaoDespesa;

            try
            {
                await _unitOfWork.CompleteAsync();
                return new Response<TipoDespesa>(existingTipoDespesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not update TipoDespesa with ID {id}.", id);
                return new Response<TipoDespesa>($"An error occurred when updating the TipoDespesa: {ex.Message}");
            }
        }

        public async Task<Response<TipoDespesa>> DeleteAsync(int id)
        {
            var existingTipoDespesa = await _TipoDespesaRepository.FindByIdAsync(id);
            if (existingTipoDespesa == null)
            {
                return new Response<TipoDespesa>("TipoDespesa not found.");
            }

            try
            {
                _TipoDespesaRepository.Remove(existingTipoDespesa);
                await _unitOfWork.CompleteAsync();

                return new Response<TipoDespesa>(existingTipoDespesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not delete TipoDespesa with ID {id}.", id);
                return new Response<TipoDespesa>($"An error occurred when deleting the TipoDespesa: {ex.Message}");
            }
        }
    }
}

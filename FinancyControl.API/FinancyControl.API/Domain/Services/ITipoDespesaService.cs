using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Services.Communication;

namespace FinancyControl.API.Domain.Services
{
    public interface ITipoDespesaService
    {
        Task<IEnumerable<TipoDespesa>> ListAsync();
        Task<Response<TipoDespesa>> SaveAsync(TipoDespesa tipoDespesa);
        Task<Response<TipoDespesa>> UpdateAsync(int id, TipoDespesa tipoDespesa);
        Task<Response<TipoDespesa>> DeleteAsync(int id);
    }
}

using FinancyControl.API.Domain.Models;

namespace FinancyControl.API.Domain.Repositories
{
    public interface ITipoDespesaRepository
    {
        Task<IEnumerable<TipoDespesa>> ListAsync();
        Task AddAsync(TipoDespesa tipoDespesa);
        Task<TipoDespesa?> FindByIdAsync(int id);
        void Update(TipoDespesa tipoDespesa);
        void Remove(TipoDespesa tipoDespesa);
    }
}

using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Models.Queries;

namespace FinancyControl.API.Domain.Repositories
{
    public interface IDespesaRepository
    {
        Task<QueryResult<Despesa>> ListAsync(DespesaQuery query);
        Task AddAsync(Despesa despesa);
        Task<Despesa?> FindByIdAsync(int id);
        void Update(Despesa despesa);
        void Remove(Despesa despesa);
    }
}

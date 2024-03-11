using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Models.Queries;
using FinancyControl.API.Domain.Services.Communication;

namespace FinancyControl.API.Domain.Services
{
    public interface IDespesaService
    {
        Task<QueryResult<Despesa>> ListAsync(DespesaQuery query);
        Task<Response<Despesa>> SaveAsync(Despesa despesa);
        Task<Response<Despesa>> UpdateAsync(int id, Despesa despesa);
        Task<Response<Despesa>> DeleteAsync(int id);
    }
}

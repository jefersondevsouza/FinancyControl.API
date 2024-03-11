using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Models.Queries;
using FinancyControl.API.Domain.Repositories;
using FinancyControl.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FinancyControl.API.Persistence.Repositories
{
    public class DespesaRepository(AppDbContext context) : BaseRepository(context), IDespesaRepository
    {
        public async Task<QueryResult<Despesa>> ListAsync(DespesaQuery query)
        {
            IQueryable<Despesa> queryable = _context.Despesas
                                                    .Include(p => p.TipoDespesa)
                                                    .AsNoTracking();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            if (query.TipoDespesaId.HasValue && query.TipoDespesaId > 0)
            {
                queryable = queryable.Where(p => p.TipoDespesaId == query.TipoDespesaId);
            }

            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();

            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            List<Despesa> Despesas = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client-side calculations ).
            return new QueryResult<Despesa>
            {
                Items = Despesas,
                TotalItems = totalItems,
            };
        }

        public async Task<Despesa?> FindByIdAsync(int id)
            => await _context.Despesas.Include(p => p.TipoDespesa).FirstOrDefaultAsync(p => p.Id == id); // Since Include changes the method's return type, we can't use FindAsync

        public async Task AddAsync(Despesa Despesa)
            => await _context.Despesas.AddAsync(Despesa);

        public void Update(Despesa Despesa)
        {
            _context.Despesas.Update(Despesa);
        }

        public void Remove(Despesa Despesa)
        {
            _context.Despesas.Remove(Despesa);
        }
    }
}

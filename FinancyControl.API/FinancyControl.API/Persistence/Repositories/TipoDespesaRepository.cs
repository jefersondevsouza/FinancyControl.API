using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Repositories;
using FinancyControl.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FinancyControl.API.Persistence.Repositories
{
    public class TipoDespesaRepository(AppDbContext context) : BaseRepository(context), ITipoDespesaRepository
    {

        // "AsNoTracking" tells Entity Framework that it is not necessary to track changes for listed entities. This makes code run faster.
        public async Task<IEnumerable<TipoDespesa>> ListAsync()
            => await _context.TiposDespesas.AsNoTracking().ToListAsync();

        public async Task AddAsync(TipoDespesa tipoDespesa)
            => await _context.TiposDespesas.AddAsync(tipoDespesa);

        public async Task<TipoDespesa?> FindByIdAsync(int id)
            => await _context.TiposDespesas.FindAsync(id);

        public void Update(TipoDespesa tipoDespesa)
        {
            _context.TiposDespesas.Update(tipoDespesa);
        }

        public void Remove(TipoDespesa tipoDespesa)
        {
            _context.TiposDespesas.Remove(tipoDespesa);
        }
    }
}

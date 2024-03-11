using FinancyControl.API.Domain.Repositories;
using FinancyControl.API.Persistence.Contexts;

namespace FinancyControl.API.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Data.Contracts
{
    public class DbContextProvider<TContext> : IDbContextProvider
        where TContext : DbContext
    {
        private readonly DbContext _context;

        public DbContextProvider(TContext context)
        {
            _context = context;
        }

        public DbContext GetContext() => _context;

      
    }
}
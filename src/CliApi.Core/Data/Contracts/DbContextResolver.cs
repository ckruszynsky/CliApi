using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Data.Contracts
{
    public class DbContextResolver<TContext>:IDbContextResolver
        where TContext:DbContext
    {
        private readonly TContext _context;

        public DbContextResolver(TContext context)
        {
            _context = context;
        }

        public DbContext GetContext() => _context;
    }
}
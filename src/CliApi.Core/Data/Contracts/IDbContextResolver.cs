using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Data.Contracts
{
    public interface IDbContextResolver
    {
        DbContext GetContext();
    }
}
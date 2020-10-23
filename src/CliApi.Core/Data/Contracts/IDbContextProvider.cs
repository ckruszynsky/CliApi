using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Data.Contracts
{
    public interface IDbContextProvider    
    {
        DbContext GetContext();
    }
}
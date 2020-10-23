using System.Collections.Generic;

namespace CliApi.Core.Data.Contracts
{
    public interface IDataSeeder
    {
        List<string> ExcludedEnvironments { get; set; }
        void Seed(IDbContextProvider provider);
    }
}
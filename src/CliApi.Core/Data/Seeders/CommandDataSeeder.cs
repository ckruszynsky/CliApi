using System.Collections.Generic;
using CliApi.Core.Data.Contracts;
using CliApi.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Data.Seeders
{
    public class CommandDataSeeder : JsonDataSeeder<Command>
    {
        public override List<string> ExcludedEnvironments { get; set; } = new List<string>
        {
            "Testing"            
        };
       
    }
}
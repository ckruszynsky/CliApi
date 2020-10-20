using CliApi.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using CliApi.Web.Models;

namespace CliApi.Web.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}
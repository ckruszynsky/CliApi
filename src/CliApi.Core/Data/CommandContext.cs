using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CommandContext).GetTypeInfo().Assembly
            );
        }

          public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
           OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (!(entry.Entity is IEntity entity)) continue;
                var now = DateTime.UtcNow;                    
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entity.LastModifiedDate = now;                            
                        break;

                    case EntityState.Added:
                        entity.CreatedDate = now;
                        entity.LastModifiedDate = now;                            
                        break;
                }
            }
        }

    }
}
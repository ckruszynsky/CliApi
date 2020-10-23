using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CliApi.Core.Domain.Models;

namespace CliApi.Core.Data.Configurations
{
    public class CommandConfiguration : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {

            builder.ToTable("Command");
            builder.HasKey(cmd => cmd.Id);

            builder.Property(cmd => cmd.HowTo)
            .HasMaxLength(250)
            .IsRequired(true);

            builder.Property(cmd => cmd.Platform)
            .IsRequired(true);

            builder.Property(cmd => cmd.CommandLine)
            .IsRequired(true);

        }
    }
}
using System.Collections.Generic;
using System.Linq;
using CliApi.Core.Domain.Models;

namespace CliApi.Core.Data
{
    public class MockCommandRepository : ICommandRepository
    {
        private IEnumerable<Command> _commands = new List<Command>
        {
            new Command{
                Id=0,
                HowTo="How to generate a migration",
                CommandLine="dotnet ef migrations add <Name of Migration>",
                Platform=".Net Core EF"
            },
            new Command{
                Id=1,
                HowTo="Run Migrations",
                CommandLine="dotnet ef database update",
                Platform=".Net Core EF"
            },
            new Command
            {
                Id = 2,
                HowTo = "List active migrations",
                CommandLine = "dotnet ef migrations list",
                Platform = ".Net Core EF"
            }
        };

        public void Create(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAll()
        {

            return _commands;
        }

        public Command GetById(int id)
        {
            return _commands.FirstOrDefault(command => command.Id == id);
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}
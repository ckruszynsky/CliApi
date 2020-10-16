using System.Collections.Generic;
using CliApi.Web.Models;
using System.Linq;

namespace CliApi.Web.Data
{
    public class SqlCommandRepository : ICommandRepository
    {
        private readonly CommandContext _context;

        public SqlCommandRepository(CommandContext context)
        {
            _context = context;
        }

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
            return _context.Commands.ToList();
        }

        public Command GetById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
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
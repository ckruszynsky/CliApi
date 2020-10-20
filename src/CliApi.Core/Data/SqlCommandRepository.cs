using System;
using System.Collections.Generic;
using System.Linq;
using CliApi.Core.Domain.Models;

namespace CliApi.Core.Data
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
            if (command == null)
            {
                throw new ArgumentException(nameof(command));
            }
            _context.Commands.Add(command);
        }

        public void Delete(Command command)
        {
            if (command == null)
            {
                throw new ArgumentException(nameof(command));
            }

            _context.Commands.Remove(command);

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
            return (_context.SaveChanges() >= 0);
        }

        public void Update(Command command)
        {
            //no implementation needed.
        }
    }
}
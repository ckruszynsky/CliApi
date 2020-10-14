using System.Collections.Generic;
using CliApi.Web.Models;

namespace CliApi.Web.Data
{
    public interface ICommandRepository
    {
         bool SaveChanges();
         IEnumerable<Command> GetAll();
         Command GetById(int id);
         void Create(Command command);
         void Update(Command command);
         void Delete(Command command);
    }
}
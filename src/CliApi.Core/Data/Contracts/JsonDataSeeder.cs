using System.Collections.Generic;
using System.IO;
using System.Linq;
using CliApi.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CliApi.Core.Data.Contracts
{
     public abstract class JsonDataSeeder<TEntity> : IDataSeeder where TEntity : class, IEntity
    {
        public virtual List<string> ExcludedEnvironments { get; set; } = new List<string>
        {
            "Production"
        };

        protected virtual void SeedDependentObjects(DbContext context)
        {

        }

        public void Seed(IDbContextProvider provider)
        {
            var context = provider.GetContext();
            SeedDependentObjects(context);

            if (context.Set<TEntity>().Any()) return;

            var data = GetDataFromFile<TEntity>();

            if (!data.Any()) return;

            foreach (var entity in data)
            {
                context.Attach(entity);
                context.Entry(entity).State = EntityState.Added;
            }
            context.SaveChanges();
        }
        
        private static List<T> GetDataFromFile<T>()
        {
            var name = typeof(T).Name;
            var currentDirectory = Directory.GetCurrentDirectory();
            var contractFile = Path.Combine(currentDirectory, "seed" + Path.DirectorySeparatorChar + name + ".json");

            if (!File.Exists(contractFile)) return new List<T>();

            var contractTypes = JsonConvert.DeserializeObject<List<T>>
            (
                File.ReadAllText(contractFile)
            );
            return contractTypes;
        }


    }
}
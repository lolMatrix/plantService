using Entities;
using System.Data.Entity;

namespace DataBase
{
    public class Context<T> : DbContext, IModelContext<T> 
        where T : Model
    {
        public DbSet<T> Sensors { get; set; }

        public Context() : base()
        {

        }

        public T Save(T model)
        {
            model = Sensors.Add(model);
            SaveChanges();
            return model;
        }

        public void Delete(T model)
        {
            Sensors.Remove(model);
            SaveChanges();
        }
    }
}

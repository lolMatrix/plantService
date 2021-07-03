using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class SensorContext : DbContext, IModelContext<Sensor>
    {
        public DbSet<Sensor> Sensors { get; set; }

        public SensorContext() : base()
        {

        }

        public Sensor Save(Sensor model)
        {
            model = Sensors.Add(model);
            SaveChanges();
            return model;
        }

        public void Delete(Sensor model)
        {
            Sensors.Remove(model);
            SaveChanges();
        }
    }
}

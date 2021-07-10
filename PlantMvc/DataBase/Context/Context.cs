using Entities;
using Entities.Models;
using System.Data.Entity;

namespace DataBase
{
    public class Context : DbContext
    {
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Greenhose> Greenhoses { get; set; }
        public DbSet<GardenBed> GardenBeds { get; set; }
        public DbSet<ConfigurationItem> ConfigurationItems { get; set; }

        public Context() : base("Server=(localdb)\\mssqllocaldb;Database=PlantDB;Trusted_Connection=True;")
        {
        }
    }
}

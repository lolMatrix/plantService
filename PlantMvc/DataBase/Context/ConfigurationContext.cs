using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ConfigurationContext : DbContext
    {
        public ConfigurationContext() : base()
        {

        }

        public DbSet<ConfigurationItem> Items { get; set; }
    }
}

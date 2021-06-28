using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
     public class GardenBed
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal WaterValuem { get; set; }

        public Greenhose Greenhose { get; set; }

        public List<Sensor> Sensors { get; set; }

    }
}

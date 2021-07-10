using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
     public class GardenBed : Model
     {
        public string Name { get; set; }

        public decimal WaterVolume { get; set; }

        public int GreenhoseId { get; set; }
        public virtual Greenhose Greenhose { get; set; }
        
     }
}

using Entities.Enums;
using System.Collections.Generic;

namespace Entities.Models
{
    
    public class Sensor : Model
    {
        public string Name { get; set; }

        public SensorType Type { get; set; }

        public int BedId { get; set; }
        public virtual GardenBed Bed { get; set; }
    }
}
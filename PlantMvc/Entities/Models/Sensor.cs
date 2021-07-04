using Entities.Enums;
using System.Collections.Generic;

namespace Entities.Models
{
    
    public class Sensor : Model
    {
        public string Name { get; set; }

        public SensorType Type { get; set; }

        public List<Data> SensorData { get; set; }

        public GardenBed Bed { get; set; }
    }
}
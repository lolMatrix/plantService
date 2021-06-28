using Entities.Enums;

namespace Entities.Models
{
    
    public class Sensor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SensorType Type { get; set; }

        public decimal SensorData { get; set; }

        public GardenBed Bed { get; set; }
    }
}
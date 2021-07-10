using System;

namespace Entities.Models
{
    public class Data : Model
    {
        public int Value { get; set; }

        public string MeseasurementUnit { get; set; }

        public DateTime TimeMeasurement { get; set; }

        public int SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}

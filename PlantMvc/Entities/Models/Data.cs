using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Data : Model
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public string MeseasurementUnit { get; set; }
        [Required]
        public DateTime TimeMeasurement { get; set; }
        [Required]
        public int SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
    }
}

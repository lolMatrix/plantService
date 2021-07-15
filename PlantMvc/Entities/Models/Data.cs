using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual Sensor Sensor { get; set; }
    }
}

using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Models
{

    public class Sensor : Model
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public SensorType Type { get; set; }

        [Required]
        public int BedId { get; set; }
        [JsonIgnore]
        public virtual GardenBed Bed { get; set; }
    }
}
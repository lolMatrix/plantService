using Entities.Enums;
using System.ComponentModel.DataAnnotations;

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
        public virtual GardenBed Bed { get; set; }
    }
}
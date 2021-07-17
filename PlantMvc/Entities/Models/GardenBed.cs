using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class GardenBed : Model
     {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal WaterVolume { get; set; }
        [Required]
        public int GreenhoseId { get; set; }
        [JsonIgnore]
        public virtual Greenhose Greenhose { get; set; }
        
     }
}

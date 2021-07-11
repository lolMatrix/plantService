using System.ComponentModel.DataAnnotations;

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
        public virtual Greenhose Greenhose { get; set; }
        
     }
}

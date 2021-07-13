using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class ConfigurationItem : Model
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Value { get; set; }
    }
}

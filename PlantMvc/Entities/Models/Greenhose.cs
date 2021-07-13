using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Greenhose : Model
    {

        /// <example>Van DarkHolm Greenhose</example>
        [Required]
        public string Name { get; set; }
        
        /// <example>
        /// Japan, Tokio, Gachi street, 10
        /// </examole>
        [Required]
        public string Location { get; set; }

    }
}

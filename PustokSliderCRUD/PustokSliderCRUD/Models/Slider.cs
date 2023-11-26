using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokSliderCRUD.Models
{
    public class Slider : BaseEntity
    {

        [Required]
        [StringLength(maximumLength: 60)]
        public string UpperText { get; set; }
        [Required]
        [StringLength(maximumLength: 120)]
        public string LowerText { get; set; }
        public string RedirectUrl { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string RedirectText { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

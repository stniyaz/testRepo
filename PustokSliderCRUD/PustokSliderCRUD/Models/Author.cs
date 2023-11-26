using System.ComponentModel.DataAnnotations;

namespace PustokSliderCRUD.Models
{
    public class Author : BaseEntity
    {
        [Required]
        [StringLength(maximumLength:60)]
        public string Fullname { get; set; }
        public List<Book>? Books { get; set; }

    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
   public class CoverType
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Cover name")]
        public string Name { get; set; }
    }
}

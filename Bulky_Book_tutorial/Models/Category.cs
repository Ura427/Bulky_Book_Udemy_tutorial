using System.ComponentModel.DataAnnotations;

namespace Bulky_Book_tutorial.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

    }
}

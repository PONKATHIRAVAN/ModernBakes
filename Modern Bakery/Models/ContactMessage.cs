using System.ComponentModel.DataAnnotations;

namespace BakeryShop.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
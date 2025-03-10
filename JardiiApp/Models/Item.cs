using System.ComponentModel.DataAnnotations;

namespace JardiiApp.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public Decimal Value { get; set; }

        public string Description { get; set; }

    }
}

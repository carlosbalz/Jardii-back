using JardiiApp.Models.Type;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JardiiApp.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int Number { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public Decimal Value { get; set; }

        [Required]
        public List<Item> Items { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class Cart
    {
        [Editable(false)]
        public int Id { get; set; }

        [MaxLength(32)]
        [MinLength(1)]
        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int UserId { get; set; }

        public DateTime Added { get; set; } = DateTime.Now;
        public AppUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        [Range(0, double.MaxValue)]
        internal double total => CartItems.Sum(c => c.Product.Price * c.Quantity);
    }
}
using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class Cart
    {
        [Editable(false)]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        [Range(0, double.MaxValue)]

        internal double total => CartItems.Sum(c => c.Product.Price * c.Quantity);
    }
}

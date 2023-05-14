using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class Cart
    {
        [Editable(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        
        internal double total => CartItems.Sum(c => c.Product.Price * c.Quantity);
    }
}

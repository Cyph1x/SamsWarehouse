using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class Product
    {
        [Editable(false)]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string? Details { get; set; }
        public string? Ingredients { get; set; }
        public int images { get; set; }
        public ICollection<CartItem> CartItems { get; set;}
    }
}

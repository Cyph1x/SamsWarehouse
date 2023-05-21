using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class Product
    {
        [Editable(false)]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [MaxLength(255)]
        public string Size { get; set; }
        [MaxLength(4096)]
        public string? Details { get; set; }
        [MaxLength(4096)]
        public string? Ingredients { get; set; }
        [Range(0, int.MaxValue)]
        public int images { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}

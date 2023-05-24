using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class CartItem
    {
        [Editable(false)]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Range(0, int.MaxValue)]
        public int CartId { get; set; }
        [Range(0, int.MaxValue)]
        public int ProductId { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class CartItem
    {
        [Editable(false)]
        
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;

        public int Quantity { get; set; } = 1;
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}

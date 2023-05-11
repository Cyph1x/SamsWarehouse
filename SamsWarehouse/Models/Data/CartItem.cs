namespace SamsWarehouse.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;


        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}

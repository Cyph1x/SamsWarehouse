namespace SamsWarehouse.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}

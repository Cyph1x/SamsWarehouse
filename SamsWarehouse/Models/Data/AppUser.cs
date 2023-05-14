using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class AppUser
    {
        [Editable(false)]
        public int Id { get; set; }
        public string Username { get; set; }
        
        public string PasswordHash { get; set; }
        public int SelectedCart { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}

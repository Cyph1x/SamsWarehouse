using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class AppUser
    {
        [Editable(false)]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [MaxLength(255)]
        [MinLength(1)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string PasswordHash { get; set; }
        [Range(0, int.MaxValue)]
        public int SelectedCart { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}

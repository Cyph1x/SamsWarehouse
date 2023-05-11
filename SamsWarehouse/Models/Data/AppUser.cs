﻿namespace SamsWarehouse.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}

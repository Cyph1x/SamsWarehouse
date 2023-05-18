using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models.Data
{
    public class LoginDTO
    {
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(255)]
        [MinLength(1)]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(255)]
        [MinLength(8)]
        public string Password { get; set; }

    }
}

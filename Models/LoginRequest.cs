using System.ComponentModel.DataAnnotations;

namespace Resturant.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string RememberMe { get; set; }
    }
}

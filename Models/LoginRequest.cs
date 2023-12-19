using System.ComponentModel.DataAnnotations;

namespace Resturant.Models
{
    public class LoginRequest
    {
        private string rememberMe;

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string RememberMe { 
            get { return rememberMe ?? "off"; } 
            set => rememberMe = value; 
        }
    }
}

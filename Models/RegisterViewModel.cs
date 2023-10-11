using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace Resturant.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        //Used for feedback to the user if the registration was successfull or not.
        public IdentityResult Result { get; set; }
    }
}

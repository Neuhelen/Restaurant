using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Resturant.Models
{
    public class Register
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = string.Empty;

        public List<IdentityRole> RoleList { get; set; } = new List<IdentityRole>();

        // A short text stating if the user registration was successfull.
        public string StatusDescription { get; set; }

        // More detailed feedback to the user if the registration was successfull or not.
        public IEnumerable<IdentityError> Errors { get; set; }

    }
}

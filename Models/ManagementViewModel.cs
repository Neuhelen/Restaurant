using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Resturant.Models
{
    public class ManagementViewModel
    {
        
        public List<UserWithClaims> Users { get; set; } = new List<UserWithClaims>();


        public List<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();

        public class UserWithClaims : IdentityUser
        {
            public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
        }

        public class Edit
        {
            public string UserName { get; set; } = string.Empty;
            public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();

        }
    }
}

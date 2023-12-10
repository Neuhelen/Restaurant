using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Resturant.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authentication.Cookies;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using static Resturant.Models.ManagementViewModel;

namespace Resturant.Controllers
{
    public class UserController : Controller
    {
        private readonly ResturantContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ResturantContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Index(string? message)
        {
            return View("Index", message);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            IdentityUser user = await _userManager.FindByNameAsync(login.UserName);
            
            SignInResult result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe == "on", false);
            if (user == null || !result.Succeeded)
                return View("Index", "Login failed!");

            // Attach claims (eg. roles) to user.
            await AddClaimsToUser(user);

            return RedirectToAction("Index", "Home");
        }


        private async Task AddClaimsToUser(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            User.AddIdentity(claimsIdentity);
        }


        [HttpGet]
        //[Authorize(Roles = "admin")]
        public IActionResult Register(Register user)
        {
            if (user == null) {
                user = new Register();
            }
            user.RoleList = _roleManager.Roles.ToList();
            return View("Register", user);
        }


        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> RegisterPost(Register user)
        {
            if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                user.Password = string.Empty;
                user.StatusDescription = "Creation of user was unsuccessful.\nA user by the given name already exists!";
                return View("Register", user);
            }

            var newUser = new IdentityUser()
            {
                UserName = user.UserName,
                NormalizedUserName = user.UserName.ToLower(),
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);
            user.Password = string.Empty;
            user.Errors = result.Errors;
            if (result.Succeeded)
            {
                user.StatusDescription = "The user was created successfully!";
                await _userManager.AddToRoleAsync(newUser, user.Role);
            }
            else
                user.StatusDescription = "Something went wrong!";

            user.RoleList = _roleManager.Roles.ToList();
            return View("Register", user);
        }


        public async Task<IActionResult> Check()
        {
            // Access the current user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "None";

            // Get the user using the UserManager
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                return View("Check", "No logged in user found!".ToJson());

            var userRoles = await _userManager.GetRolesAsync(user);
            
            return View("Check", new { User = user, Roles = userRoles }.ToJson());
        }


        //[Authorize(Roles = "admin")]
        public IActionResult Management(ManagementViewModel model)
        {
            if(model == null)
                return View();

            var identityUsers = _userManager.Users.ToList();
            model.Users = identityUsers.Select(user => new UserWithClaims
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();

            model.AllRoles = _roleManager.Roles.ToList();

            return View(model);
        }


        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> Delete(string id)
        {
            if (id == "ccc95d47-24a4-4cc6-9899-4d5f596fab64")
                return new JsonResult("Admin user cannot be removed!");
            Console.WriteLine("Deleting user with id: " + id);
            IdentityResult result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            return new JsonResult(result);
        }


        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<string> Edit(string id, [FromBody] ManagementViewModel.Edit euser)
        {
            var user = _context.Users.First(x => x.Id == id);
            user.UserName = euser.UserName;
            user.NormalizedUserName = euser.UserName.ToUpper();

            var currentRoles = await _userManager.GetRolesAsync(user);
            IEnumerable<string> newroles = euser.Roles.Except(currentRoles);
            IEnumerable<string> rolesToRemove = currentRoles.Except(euser.Roles);

            // Makes sure that the admin accound does not accedentaly looses its admin rights ;)
            if (user.NormalizedUserName == "ADMIN" && rolesToRemove.Contains("admin"))
                rolesToRemove = rolesToRemove.Except(new List<string>() { "admin" });
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            await _userManager.AddToRolesAsync(user, newroles);

            EntityEntry<IdentityUser> result = _context.Update(user);
            if(await _context.SaveChangesAsync() > 0)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return "User was succesfully updated!";
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return "Something went wrong!";
        }


        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<string> CreateRole([FromBody] string newRole)
        {
            if (newRole == null || newRole == string.Empty)
            {
                Response.StatusCode = 400;
                return "Error: The request body does not contain a string with a role!";
            }

            IdentityResult? roleResult;
            var roleExist = await _roleManager.RoleExistsAsync(newRole);
            if (roleExist)
                return "Role already exist!";
            else
                roleResult = await _roleManager.CreateAsync(new IdentityRole(newRole));
            if (!roleResult?.Succeeded ?? true)
                return "Something went wrong!";
            return "Creation of role was succesfull!";
        }


        [HttpDelete]
        [Authorize(Roles ="admin")]
        public async Task<string> DeleteRole([FromBody] string roleToDelete)
        {
            if(roleToDelete == null || roleToDelete == string.Empty)
            {
                Response.StatusCode = 400;
                return "Error: The request does not contain an existing role!";
            }

            IdentityRole roleItem = _roleManager.Roles.First(role => role.Name == roleToDelete);
            IdentityResult? deleteResult = await _roleManager.DeleteAsync(roleItem);
            if (!(deleteResult?.Succeeded ?? true))
                return "Something went wrong!";
            return "Removal of role was succesfull!";
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}

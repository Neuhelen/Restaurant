using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resturant.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ResturantContext>(options =>
        {
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34)));
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });


        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ResturantContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddDefaultTokenProviders();

        builder.Services.AddRazorPages();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

            options.LoginPath = "/User/Login";
            options.AccessDeniedPath = "/User/AccessDenied";
            options.SlidingExpiration = true;
        });
        // ### Login end ###

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Create default users and roles (otherwise the /User/Management page and likewise cannot be accessed on first run on a new database)
        CreateDefaultUsersAndRoles(app.Services.CreateScope().ServiceProvider).Wait();


        app.Run();
    }

    private static async Task CreateDefaultUsersAndRoles(IServiceProvider services)
    {
        var _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var _userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Create default roles
        string[] defaultRoles = { "admin", "waiter" };
        foreach (string role in defaultRoles)
        {
            if (await _roleManager.RoleExistsAsync(role))
                continue;
            var newRoleResult = await _roleManager.CreateAsync(new IdentityRole(role));
            if (!newRoleResult.Succeeded)
                throw new Exception($"Failed to create role {role}");
        }

        // Create default admin user
        if (await _userManager.FindByNameAsync("admin") != null)
            return;

        var user = new IdentityUser("admin");
        var userResult = await _userManager.CreateAsync(user, "e2db9862-a132-4052-b037-f003f31a5c01");
        if (!userResult.Succeeded)
            throw new Exception("Failed to create admin user");

        var roleResult = await _userManager.AddToRoleAsync(user, "admin");
        if (!roleResult.Succeeded)
            throw new Exception("Failed to add admin to role admin");
    }
}
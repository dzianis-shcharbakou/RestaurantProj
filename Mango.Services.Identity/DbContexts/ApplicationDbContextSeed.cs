using IdentityModel;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Mango.Services.Identity.DbContexts
{
	public class ApplicationDbContextSeed : IApplicationDbContextSeed
	{
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationDbContextSeed(ApplicationDbContext applicationDbContext,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager, 
			IConfiguration config,
			IWebHostEnvironment webHostEnvironment)
		{
			_applicationDbContext = applicationDbContext;
			_userManager = userManager;
			_roleManager = roleManager;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

		public async Task SeedAsync()
		{
			if (_roleManager.FindByNameAsync(StaticDetails.Admin).Result == null)
			{
				_applicationDbContext.Database.Migrate();

				await SeedRolesAsync();
				await SeedUsersAsync();
			}
		}

		private async Task SeedRolesAsync()
		{
			var roles = new List<IdentityRole>
			{
				new IdentityRole { Name = StaticDetails.Admin },
				new IdentityRole { Name = StaticDetails.User }
			};

			foreach (var role in roles)
			{
				if (!await _roleManager.RoleExistsAsync(role.Name))
				{
					await _roleManager.CreateAsync(role);
				}
			}
		}

		private async Task SeedUsersAsync()
		{
			var adminUser = new ApplicationUser
			{
				UserName = "admin1@gmail.com",
				Email = "admin1@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "1234567890",
				FirstName = "Ben",
				LastName = "Admin",
			};

			if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
			{
				var adminPassword = string.Empty;
				if (_webHostEnvironment.IsDevelopment())
				{
					adminPassword = _config["Credentials:AdminPassword"];
				}
                else
                {
                    throw new NotImplementedException();
                }

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(adminUser, StaticDetails.Admin);
					await _userManager.AddClaimsAsync(adminUser, new Claim[]
					{
						new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
						new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
						new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
						new Claim(JwtClaimTypes.Role, StaticDetails.Admin),
					});
				}
			}

			var user = new ApplicationUser
			{
				UserName = "customer1@gmail.com",
				Email = "customer1@gmail.com",
				EmailConfirmed = true,
				PhoneNumber = "1234567890",
				FirstName = "Ben",
				LastName = "Cust",
			};

			if (await _userManager.FindByEmailAsync(user.Email) == null)
			{
                var customerPassword = string.Empty;
                if (_webHostEnvironment.IsDevelopment())
                {
                    customerPassword = _config["Credentials:CustomerPassword"];
                }
				else
				{
					throw new NotImplementedException();
				}

                var result = await _userManager.CreateAsync(user, customerPassword);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, StaticDetails.User);
					await _userManager.AddClaimsAsync(user, new Claim[]
					{
						new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
						new Claim(JwtClaimTypes.GivenName, user.FirstName),
						new Claim(JwtClaimTypes.FamilyName, user.LastName),
						new Claim(JwtClaimTypes.Role, StaticDetails.User),
					});
				}
			}
		}
	}
}

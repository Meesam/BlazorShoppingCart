using BlazorShoppingCart.Models;
using BlazorShoppingCart.Models.DTO;
using BlazorShoppingCart.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShoppingCart.BusinessService.Authentication
{
    public class UserAuth : IUserAuth
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserAuth(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResponse> LoginUser(LoginVM loginVM)
        {
            if (loginVM == null) { return null; }
            var user = await _userManager.FindByNameAsync(loginVM.Username);
            var password = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (user != null && password)
            {
                var role = await _userManager.GetRolesAsync(user);
                return new LoginResponse
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    Email = user.Email,
                    Role = role.FirstOrDefault()
                };
            }
            return null;
        }

        public async Task<bool> RegisterUser(RegisterVM? registerVM)
        {
            if (registerVM == null)
            {
                return false;
            }
            var user = new AppUser
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.Email,
                DateOfBirth = registerVM.DateOfBirth,
            };

            if (await _roleManager.RoleExistsAsync("Customer"))
            {
                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if (!result.Succeeded) { return false; }
                var userRole = await _userManager.AddToRoleAsync(user, "Customer");
                if (!userRole.Succeeded) { return false; }
                return true;
            }
            return false;
        }
    }
}

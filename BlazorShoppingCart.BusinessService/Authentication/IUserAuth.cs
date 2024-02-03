using BlazorShoppingCart.Models.DTO;
using BlazorShoppingCart.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShoppingCart.BusinessService.Authentication
{
    public interface IUserAuth
    {
        Task<bool> RegisterUser(RegisterVM registerVM);
        Task<LoginResponse> LoginUser(LoginVM loginVM);
    }
}

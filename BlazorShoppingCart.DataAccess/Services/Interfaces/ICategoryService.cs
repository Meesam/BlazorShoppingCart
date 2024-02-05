using BlazorShoppingCart.Models;
using BlazorShoppingCart.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShoppingCart.DataAccess.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse?> AddCategory(Category category);

        Task<CategoryResponse?> GetCategoryById(int id);

        Task<bool> DeleteCategory(int id);

        Task<IQueryable<CategoryResponse>>? GetAllCategories();

        Task<CategoryResponse> UpdateCategory(Category category);

    }
}

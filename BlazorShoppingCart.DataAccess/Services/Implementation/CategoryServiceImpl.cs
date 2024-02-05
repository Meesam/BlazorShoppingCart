using BlazorShoppingCart.DataAccess.Services.Interfaces;
using BlazorShoppingCart.Models;
using BlazorShoppingCart.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace BlazorShoppingCart.DataAccess.Services.Implementation
{
    public class CategoryServiceImpl : ICategoryService
    {

        private readonly ShoppingCartContext _shoppingCartContext;
        public CategoryServiceImpl(ShoppingCartContext shoppingCartContext)
        {

            _shoppingCartContext = shoppingCartContext;

        }

        public async Task<CategoryResponse?> AddCategory(Category category)
        {
            try
            {
                if (category is null) { return null; }
                await _shoppingCartContext.Categories.AddAsync(category);
                await _shoppingCartContext.SaveChangesAsync();
                var result = await _shoppingCartContext.Categories.OrderByDescending(c => c.CreatedDate).FirstOrDefaultAsync();
                if (result is null) { return null; }
                return new CategoryResponse
                {
                    Id = result.Id,
                    Title = result.Title,
                    Type = result.Type,
                    Description = result.Description,
                    CreatedDate = result.CreatedDate,
                    CreatedBy = result.CreatedBy
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                if (id is 0) return false;
                var category = await _shoppingCartContext.Categories.FindAsync(id);
                if (category is null) return false;
                _shoppingCartContext.Categories.Remove(category);
                await _shoppingCartContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Task<IQueryable<CategoryResponse>>? GetAllCategories()
        {
            try
            {
                var records = from c in _shoppingCartContext.Categories
                              join u in _shoppingCartContext.Users on c.CreatedBy equals u.Id
                              select new CategoryResponse
                              {
                                  Id = c.Id,
                                  Title = c.Title,
                                  Type = c.Type,
                                  Description = c.Description,
                                  CreatedDate = c.CreatedDate,
                                  CreatedBy = u.Name
                              };
                return Task.FromResult(records);
            }
            catch (Exception)
            {
                return null;
            }



        }

        public async Task<CategoryResponse?> GetCategoryById(int id)
        {
            if (id is 0) return null;
            var category = await _shoppingCartContext.Categories.FindAsync(id);
            if (category is null) return null;
            return new CategoryResponse
            {
                Id = category.Id,
                Title = category.Title,
                Type = category.Type,
                Description = category.Description,
                CreatedDate = category.CreatedDate,
                CreatedBy = category.CreatedBy
            };
        }

        public Task<CategoryResponse> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }


    }
}

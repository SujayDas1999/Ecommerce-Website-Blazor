using Ecom.Server.Data;
using Ecom.Server.Services.Interface;
using Ecom.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Category>>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var response = new ServiceResponse<List<Category>>();

            if (categories == null)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = "No Categories Found!";

                return response;
            }
            
            response.Data = categories;
            response.Success = true;
            response.Message = "Categories Found!";
            response.Status = 200;
            return response;
        }
    }
}

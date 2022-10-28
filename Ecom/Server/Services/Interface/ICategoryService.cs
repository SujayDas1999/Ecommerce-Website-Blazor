using Ecom.Shared;

namespace Ecom.Server.Services.Interface
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetAllCategories();
    }
}

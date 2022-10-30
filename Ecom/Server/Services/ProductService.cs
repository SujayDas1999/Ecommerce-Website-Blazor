using Ecom.Server.Data;
using Ecom.Server.Services.Interface;
using Ecom.Shared;
using Ecom.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Variants)
                .ThenInclude(v => v.ProductType)
                .ToListAsync();

            var response = new ServiceResponse<List<Product>>
            {
                Data = products,
                Status = 200,
                Success = true
                
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Variants)
                .ThenInclude(p => p.ProductType)
                .SingleOrDefaultAsync(x => x.Id == id);
            var response = new ServiceResponse<Product>();

            if (product == null) return response = new ServiceResponse<Product> 
            { 
                Success = false, 
                Message = $"No Product Found with Id={id}", 
                Status = 404 
            };

            return response = new ServiceResponse<Product> 
            { Success = true, 
                Data=product, 
                Message = $"Product Found with Id={id}", 
                Status = 200 
            };

        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryName)
        {
            var products = await _context.Products
                .Include(c=> c.Category)
                .Include(p => p.Variants)
                .ThenInclude(p => p.ProductType)
                .Where(c => c.Category.Url == categoryName.ToLower())
                .ToListAsync();

            var response = new ServiceResponse<List<Product>>();

            if(products.Count() == 0 || products == null)
            {
                response.Status = 404;
                response.Success = false;
                response.Message = "No Products with the specific category found!";

                return response;
            }

            response.Data = products;
            response.Status = 200;
            response.Success = true;
            response.Message = "Products with the specific category found!";
            return response;
        }

        public async Task<ServiceResponse<ProductSearchResultsDto>> GetSearchedProducts(string searchText, int page)
        {
            var results = await FindProductsBySearchText(searchText);

            var pageResults = 2f;
            var pageCount = Math.Ceiling(results.Count() / pageResults);

            var products = await _context.Products
                .Include(c => c.Category)
                .Include(v => v.Variants)
                .ThenInclude(t => t.ProductType)
                .Where(x => x.Title.ToLower().Contains(searchText.ToLower())
                ||
                x.Description.ToLower().Contains(searchText.ToLower()))
                .Skip((page - 1) * (int)pageResults).Take((int)pageResults)
                .ToListAsync();

            var returnResponse = new ServiceResponse<ProductSearchResultsDto>();

            if(results.Count() == 0 || results == null)
            {
                returnResponse.Status = 404;
                returnResponse.Success = false;
                returnResponse.Message = "No Products with such Name/Desc found!";

                return returnResponse;
            }

            returnResponse.Data = new ProductSearchResultsDto
            {
                Products = products,
                CurrentPage = page,
                TotalPages = (int)pageCount
            };
            returnResponse.Success= true;
            returnResponse.Status = 200;
            returnResponse.Message = $"Total {results.Count()} products found!";
            return returnResponse;
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);
            List<string> result = new List<string>();

            foreach(var product in products)
            {
                if(product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if(!String.IsNullOrEmpty(product.Description))
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split().Select(s => s.Trim(punctuation)).ToArray();

                    foreach(var word in words)
                    {
                        if(word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            if(result.Count() == 0 || result == null)
            {
                return new ServiceResponse<List<string>>
                {
                    Status = 404,
                    Message = "No Products Found!",
                    Success = false
                };
            }

            return new ServiceResponse<List<string>>
            {
                Data = result,
                Status = 200,
                Success = true,
                Message = "Products Found!"
            };
        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                .Include(c => c.Category)
                .Include(v => v.Variants)
                .ThenInclude(t => t.ProductType)
                .Where(x => x.Title.ToLower().Contains(searchText.ToLower())
                ||
                x.Description.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var results = await _context.Products
                .Include(c => c.Category)
                .Include(v => v.Variants)
                .ThenInclude(p => p.ProductType)
                .ToListAsync();

            if(results.Count() == 0 || results == null)
            {
                return new ServiceResponse<List<Product>>
                {
                    Message = "No featured products found!",
                    Status = 404,
                    Success = false
                };
            }

            return new ServiceResponse<List<Product>>
            {
                Data= results,
                Message = $"Found {results.Count()} numbers of featured products!",
                Status = 200,
                Success = true
            };
        }
    }
}

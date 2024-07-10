using Pagination.EntityFrameworkCore.Extensions;
using WebApplication1.Models;
using DataLibrary.Models;
using DataLibrary.Dtos.GetAll;
namespace WebApplication1.Services.ProductService
{
    public interface IProductService
    {
       // Task<ServiceResponse<List<Products>>> GetAllProducts();
        Task<ServiceResponse<Products>> GetProductById(int id);
        Task<ServiceResponse<Products>> AddProduct(Products product);
        Task<ServiceResponse<Products>> UpdateProduct(Products updateproduct);
        Task<ServiceResponse<Products>> DeleteProduct(int id);
        Task<List<Products>> GetProductsAsync(GetAllDto getAllDto);
    }
}

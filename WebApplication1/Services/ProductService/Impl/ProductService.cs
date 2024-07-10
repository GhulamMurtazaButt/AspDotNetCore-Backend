using WebApplication1.Models;
using WebApplication1.Strings;
using Pagination.EntityFrameworkCore.Extensions;
using DataLibrary.Models;
using DataLibrary.Repositry;
using DataLibrary.Dtos.GetAll;
namespace WebApplication1.Services.ProductService.Impl
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepositry<Products> _genericRepositry;
        public ProductService(IGenericRepositry<Products> genericRepositry)
        {
            _genericRepositry = genericRepositry;
        }

        //public async Task<ServiceResponse<List<Products>>> GetAllProducts()
        //{
        //    ServiceResponse<List<Products>> newserviceResponse = new ServiceResponse<List<Products>>();
        //    List<Products> dbproducts = await _genericRepositry.GetAllAsync();
        //    newserviceResponse.data = dbproducts;
        //    return newserviceResponse;
        //}

        public async Task<ServiceResponse<Products>> GetProductById(int id)
        {
            ServiceResponse<Products> newserviceResponse = new ServiceResponse<Products>();
            newserviceResponse.data = await _genericRepositry.GetByIdAsync(id);
            if (newserviceResponse.data == null)
            {
                newserviceResponse.success = false;
                newserviceResponse.message = ErrorStrings.ProductNotFound;
            }
            return newserviceResponse;
        }

        public async Task<ServiceResponse<Products>> AddProduct(Products product)
        {
            ServiceResponse<Products> newserviceResponse = new ServiceResponse<Products>();
            try
            {
                newserviceResponse.data = await _genericRepositry.AddAsync(product);
                _genericRepositry.SaveChanges();
                if (newserviceResponse.data == null)
                {
                    newserviceResponse.success = false;
                    newserviceResponse.message = ErrorStrings.NothingFound;
                }
            }
            catch (Exception ex)
            {
                newserviceResponse.success = false;
                newserviceResponse.message = ex.Message;
            }
            return newserviceResponse;
        }

        public async Task<ServiceResponse<Products>> UpdateProduct(Products updateproduct)
        {
            ServiceResponse<Products> newserviceResponse = new ServiceResponse<Products>();
            try
            {
                Products newproduct = await _genericRepositry.UpdateAsync(updateproduct);
                if (newproduct != null)
                {
                    _genericRepositry.SaveChanges();
                    newserviceResponse.data = newproduct;
                    newserviceResponse.message = ErrorStrings.ProductUpdatedSuccess;
                }
                else
                {
                    newserviceResponse.success = false;
                    newserviceResponse.message = ErrorStrings.ProductNotFound;
                }
            }
            catch (Exception exp)
            {
                newserviceResponse.success = false;
                newserviceResponse.message = exp.Message;
            }
            return newserviceResponse;
        }
        public async Task<ServiceResponse<Products>> DeleteProduct(int id)
        {
            ServiceResponse<Products> newserviceResponse = new ServiceResponse<Products>();
            try
            {
                Products product = await _genericRepositry.GetByIdAsync(id);
                if (product != null)
                {
                    await _genericRepositry.DeleteAsync(product);
                    _genericRepositry.SaveChanges();
                    newserviceResponse.data = product;
                    newserviceResponse.message = ErrorStrings.ProductDeletedSuccess;
                }
                else
                {
                    newserviceResponse.success = false;
                    newserviceResponse.message = ErrorStrings.ProductNotFound;
                }
              //  List<Products> dbproducts = await _genericRepositry.GetAllAsync();
               // newserviceResponse.data = dbproducts;
            }
            catch (Exception exp)
            {
                newserviceResponse.success = false;
                newserviceResponse.message = exp.Message;
            }
            return newserviceResponse;
        }
        public async Task<List<Products>> GetProductsAsync(GetAllDto getAllDto)
        {

            List<Products> products = await _genericRepositry.GetAllAsync(getAllDto);
            return products;

            //if (searchTerm != null)
            //{
            //    products = products.Where(p =>
            //        p.Id.ToString().Contains(searchTerm.ToString()) ||
            //        p.Name.ToLower().Contains(searchTerm.ToLower()) ||
            //        p.Price.ToString().Contains(searchTerm.ToString())
            //    ).ToList();
            //}
            //switch (sortBy)
            //{
            //    case "id":
            //        products = products.OrderBy(u => u.Id).ToList();
            //        break;
            //    case "name":
            //        products = products.OrderBy(u => u.Name).ToList();
            //        break;
            //    case "price":
            //        products = products.OrderBy(u => u.Price).ToList();
            //        break;
            //    default:

            //        products = products.OrderBy(u => u.Id).ToList();
            //        break;
            //}

          //  var totalItems = products.Count;
            //var paginatedUsers = products.Skip((page - 1) * limit).Take(limit).ToList();
            //var products_ = paginatedUsers.ToList();

           // return new Pagination<Products>(products_, totalItems, page, limit);
        }

    }
}

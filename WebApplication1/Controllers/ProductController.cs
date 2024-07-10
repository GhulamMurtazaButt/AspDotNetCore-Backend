using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.ProductService;
using DataLibrary.Models;
using DataLibrary.Dtos.GetAll;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllDto getAllDto)
        {
            try
            {
                return Ok(await _productService.GetProductsAsync(getAllDto));
            }catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{

        //    return Ok(await _productService.GetAllProducts());
        //}

        [HttpGet("id")]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _productService.GetProductById(id));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

        }

        [HttpPost]

        public async Task<IActionResult> AddProduct(Products product)
        {
            try
            {
                return Ok(await _productService.AddProduct(product));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdateProduct(Products updateproduct)
        {
            try
            {
                return Ok(await _productService.UpdateProduct(updateproduct));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _productService.DeleteProduct(id));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
    }
}

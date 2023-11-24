using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI_Project.DAL;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyAppDBContext _dbContext;
        private IConfiguration _configuration;
        public ProductController(MyAppDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            try
            {
                //var products = _dbContext.Product.ToList();
               var products = _dbContext.GetAllProducts();
                if (products.Count == 0)
                {
                    return NotFound("Data Not Found.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet ("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            try
            {
                var product = _dbContext.Product.Find(Id);
                if (product == null)
                {
                    return NotFound("Product Data Not Found For the given Id");
                }
                return Ok(product);
            }
	        catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostProduct(Product Model)
        {
            try
            {
                _dbContext.Add(Model);
                _dbContext.SaveChanges();
                return Ok("Product Added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult PutProduct(Product Model)
        {
            if(Model == null || Model.Id == 0)
            {
                if(Model == null)
                {
                    return BadRequest("Model is invaild.");
                }
                else if(Model.Id == 0)
                {
                    return BadRequest($"Product id {Model.Id} is invaild.");
                }
            }
            try
            {
                var product = _dbContext.Product.Find(Model.Id);
                if (product == null)
                {
                    return NotFound($"Product id {Model.Id} is not found.");
                }
                product.Name = Model.Name;
                product.Product_type = Model.Product_type;
                product.Price = Model.Price;
                _dbContext.SaveChanges();
                return Ok("Product Detail Updated.");
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int ProductId)
        {
            try
            {
                var product = _dbContext.Product.Find(ProductId);
                if (product == null)
                {
                    return NotFound($"Product Not Found.");
                }
                _dbContext.Product.Remove(product);
                _dbContext.SaveChanges();
                return Ok("Deleted Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

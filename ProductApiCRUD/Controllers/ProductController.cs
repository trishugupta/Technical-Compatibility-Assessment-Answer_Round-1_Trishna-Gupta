using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApiCRUD.Models;
using ProductApiCRUD.ProductData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductApiCRUD.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductData _productData;
        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetProducts([FromQuery] Paginator filter)
        {
            return Ok(_productData.GetProducts(filter));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productData.GetProduct(id);
            if (product != null)
            {
                return Ok(product);
            }

          
            return NotFound($"Product with Id: {id} was not found ");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddProduct(Product product)
        {
            _productData.AddProduct(product);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + product.Id, product);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _productData.GetProduct(id);
            if (product != null)
            {
                _productData.DeleteProduct(product);
                return Ok();
            }
            return NotFound($"Product with Id: {id} was not found ");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditProduct(Guid id, Product product)
        {
            var existingproduct = _productData.GetProduct(id);
            if (existingproduct != null)
            {
                product.Id = existingproduct.Id;
                _productData.EditProduct(product);
            }
            return Ok(product);
        }
    }
}

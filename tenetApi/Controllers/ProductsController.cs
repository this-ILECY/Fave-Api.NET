using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tenet.Api.Context;
using tenetApi.Model;
using tenetApi.ViewModel;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEnumerable<ProductViewModel> _productViewModel { get; set; }
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("ProductByID")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetproductsByID([FromBody] long ProductID)
        {
            _productViewModel = _context.products.Select(c => new ProductViewModel()
            {
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                ProductCategoryID = c.ProductCategoryID,
                ProductTitle = c.ProductTitle,
                description = c.description,
                ProductCode = c.ProductCode,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ProductID == ProductID);

            if (_productViewModel == null)
            {
                return NotFound();
            }

            //return Ok();
            return _productViewModel.ToList();
            // return await _context.products.ToListAsync();
        }

        [HttpPost]
        [Route("ProductByTitle")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetproductsByTitle([FromBody] string ProductTitle)
        {
            _productViewModel = _context.products.Select(c => new ProductViewModel()
            {
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                ProductCategoryID = c.ProductCategoryID,
                ProductTitle = c.ProductTitle,
                description = c.description,
                ProductCode = c.ProductCode,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ProductTitle.Contains(ProductTitle));

            if (_productViewModel == null)
            {
                return NotFound();
            }
            //return Ok();
            return _productViewModel.ToList();
            // return await _context.products.ToListAsync();
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<ProductViewModel>> PostProduct([FromBody] ProductViewModel product)
        {
            if (_context.products.Any(c => c.ProductTitle == product.ProductTitle))
            {
                return BadRequest();
            }
            var shopId = _context.shops.FirstOrDefault(c => c.ShopID == product.ShopID);
            var catId = _context.productCategories.FirstOrDefault(c => c.ProductCategoryID == product.ProductCategoryID);

            if (shopId == null || catId == null)
            {
                return BadRequest();
            }
            Product theProduct = new Product();
            theProduct.ProductID = _context.products.Max(c => c.ProductID) + 1;
            theProduct.ProductTitle = product.ProductTitle;
            theProduct.description = product.description;
            theProduct.IsDeleted = product.IsDeleted;
            theProduct.ProductCode = product.ProductCode;
            theProduct.productCategoryFk = catId;
            theProduct.shopFk = shopId;


            _context.products.Add(theProduct);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }
        
        [HttpPut]
        [Route("ProductUpdate")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel product)
        {
            if (!_context.products.Any(c => c.ProductID == product.ProductID))
            {
                return NotFound();
            }
            var shopId = _context.shops.FirstOrDefault(c => c.ShopID == product.ShopID);
            var catId = _context.productCategories.FirstOrDefault(c => c.ProductCategoryID == product.ProductCategoryID);

            if (shopId == null || catId == null)
            {
                return BadRequest();
            }

            Product productToUpdate = _context.products.FirstOrDefault(c => c.ProductID == product.ProductID);
            productToUpdate.ProductID = product.ProductID;
            productToUpdate.ProductTitle = product.ProductTitle;
            productToUpdate.description = product.description;
            productToUpdate.IsDeleted = product.IsDeleted;
            productToUpdate.ProductCode = product.ProductCode;
            productToUpdate.productCategoryFk = catId;
            productToUpdate.shopFk = shopId;


            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPut]
        [Route("ProductDelete")]
        public async Task<IActionResult> DeleteProduct([FromBody] long productId)
        {

            if (!_context.products.Any(c => c.ProductID == productId))
            {
                return NotFound();
            }

            Product productToDelete = _context.products.FirstOrDefault(c => c.ProductID == productId);
            productToDelete.IsDeleted = true;


            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("ProductDeleteUndo")]
        public async Task<IActionResult> UnDeleteProduct([FromBody] long productId)
        {

            if (!_context.products.Any(c => c.ProductID == productId))
            {
                return NotFound();
            }

            Product productToDelete = _context.products.FirstOrDefault(c => c.ProductID == productId);

            productToDelete.IsDeleted = false;


            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}

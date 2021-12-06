using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tenetApi.Context;
using tenetApi.Model;
using tenetApi.ViewModel;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEnumerable<ProductViewModel> _productViewModel { get; set; }
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ProductByID")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetproductsByID(long ProductID)
        {
            _productViewModel = _context.products.Select(c => new ProductViewModel()
            {
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                ProductCategoryID = c.ProductCategoryID,
                ProductTitle = c.ProductTitle.Replace("_", " "),
                description = c.description,
                ProductCode = c.ProductCode,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ProductID == ProductID);

            if (_productViewModel == null)
            {
                return NotFound();
            }

            return _productViewModel.ToList();

        }

        [HttpGet]
        [Route("ProductByTitle")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetproductsByTitle(string ProductTitle)
        {
            ProductTitle = ProductTitle.ToLower();
            if (ProductTitle.Contains(" "))
            {
                ProductTitle = ProductTitle.Replace(" ", "_").ToLower();
            }
            _productViewModel = _context.products.Select(c => new ProductViewModel()
            {
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                ProductCategoryID = c.ProductCategoryID,
                ProductTitle = c.ProductTitle.Replace("_", " "),
                description = c.description,
                ProductCode = c.ProductCode,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ProductTitle.Contains(ProductTitle));

            if (_productViewModel == null)
            {
                return NotFound();
            }
            return _productViewModel.ToList();
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<ProductViewModel>> AddProduct([FromBody] ProductViewModel product)
        {
            if (product.ProductTitle.Contains(" "))
            {
                product.ProductTitle = product.ProductTitle.Replace(" ", "_").ToLower();
            }
            var shopId = await _context.shops.FirstOrDefaultAsync(c => c.ShopID == product.ShopID);
            var catId = _context.productCategories.FirstOrDefault(c => c.ProductCategoryID == product.ProductCategoryID);
            if (shopId == null)
            {
                return BadRequest("invalid shop!");
            }
            if (catId == null)
            {
                return BadRequest("invalid category!");
            }
            Product theProduct = new Product();
            theProduct.ProductTitle = product.ProductTitle;
            theProduct.description = product.description;
            theProduct.IsDeleted = product.IsDeleted;
            theProduct.ProductCode = product.ProductCode;
            theProduct.productCategoryFk = catId;
            theProduct.shopFk = shopId;
            theProduct.CreatedDate = DateTime.Now;


            _context.products.Add(theProduct);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("ProductUpdate")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel product)
        {
            if (product.ProductTitle.Contains(" "))
            {
                product.ProductTitle = product.ProductTitle.Replace(" ", "_").ToLower();
            }
            if (!_context.products.Any(c => c.ProductID == product.ProductID))
            {
                return NotFound();
            }
            var shopId = _context.shops.FirstOrDefault(c => c.ShopID == product.ShopID);
            var catId = _context.productCategories.FirstOrDefault(c => c.ProductCategoryID == product.ProductCategoryID);

            if (catId == null)
            {
                return BadRequest("Category NOT found!");
            }
            if (shopId == null)
            {
                return BadRequest("shop NOT found!");
            }

            Product productToUpdate = _context.products.FirstOrDefault(c => c.ProductID == product.ProductID);
            productToUpdate.ProductTitle = product.ProductTitle;
            productToUpdate.description = product.description;
            productToUpdate.IsDeleted = product.IsDeleted;
            productToUpdate.ProductCode = product.ProductCode;
            productToUpdate.productCategoryFk = catId;
            productToUpdate.shopFk = shopId;


            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("ProductDelete")]
        public async Task<IActionResult> DeleteProduct(long productId)
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

        [HttpPost]
        [Route("ProductDeleteUndo")]
        public async Task<IActionResult> UnDeleteProduct(long productId)
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

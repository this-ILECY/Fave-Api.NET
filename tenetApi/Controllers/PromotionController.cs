using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tenetApi.Context;
using tenetApi.Exception;
using tenetApi.Model;
using tenetApi.ViewModel;
using System.IO;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PromotionController(AppDbContext context)
        {
            _context = context;
        }



        //promotions all
        //[HttpGet]
        //[Route("PromotionAll")]
        //public async Task<ActionResult<IEnumerable<PromotionViewModel>>> GetpromotionsAll()
        //{
        //    IEnumerable<PromotionViewModel> _promotionViewModelByID;
        //    _promotionViewModelByID = _context.promotion.Select(c => new PromotionViewModel()
        //    {
        //        PromotionID = c.PromotionID,
        //        ProductID = c.ProductID,
        //        ShopID = c.ShopID,
        //        QualityGrade = c.QualityGrade,
        //        BasePrice = c.BasePrice,
        //        DiscountPrice = c.DiscountPrice,
        //        EndDate = c.EndDate,
        //        StartDate = c.StartDate,
        //        Stock = c.Stock,
        //        IsActive = c.IsActive,
        //        IsDeleted = c.IsDeleted
        //    }).ToList();

        //    if (_promotionViewModelByID == null)
        //    {
        //        return NotFound(Responses.NotFound("Promotion"));
        //    }

        //    return Ok(_promotionViewModelByID.ToList());

        //}

        [HttpGet]
        [Route("PromotionByID")]
        public async Task<ActionResult<IEnumerable<PromotionViewModel>>> GetpromotionsByID(long PromotionID)
        {
            IEnumerable<PromotionViewModel> _promotionViewModelByID;
            _promotionViewModelByID = _context.promotion.Select(c => new PromotionViewModel()
            {
                PromotionID = c.PromotionID,
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                QualityGrade = c.QualityGrade,
                BasePrice = c.BasePrice,
                DiscountPrice = c.DiscountPrice,
                EndDate = c.EndDate,
                StartDate = c.StartDate,
                Stock = c.Stock,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.PromotionID == PromotionID);

            if (_promotionViewModelByID == null)
            {
                return NotFound(Responses.NotFound("Promotion"));
            }

            return Ok(_promotionViewModelByID.ToList());

        }

        [HttpGet]
        [Route("promotionByProductID")]
        public async Task<ActionResult<IEnumerable<PromotionViewModel>>> GetpromotionsByProductID(long ProductID)
        {
            IEnumerable<PromotionViewModel> _promotionViewModelByproductID;
            _promotionViewModelByproductID = _context.promotion.Select(c => new PromotionViewModel()
            {
                PromotionID = c.PromotionID,
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                QualityGrade = c.QualityGrade,
                BasePrice = c.BasePrice,
                DiscountPrice = c.DiscountPrice,
                EndDate = c.EndDate,
                StartDate = c.StartDate,
                Stock = c.Stock,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ProductID == ProductID);

            if (_promotionViewModelByproductID == null)
            {
                return NotFound(Responses.NotFound("Promotion"));
            }
            return _promotionViewModelByproductID.ToList();
        }

        [HttpGet]
        [Route("promotionByShopID")]
        public async Task<ActionResult<IEnumerable<PromotionViewModel>>> GetpromotionsByShopID(long ShopID)
        {
            IEnumerable<PromotionViewModel> _promotionViewModelByShopID;
            _promotionViewModelByShopID = _context.promotion.Select(c => new PromotionViewModel()
            {
                PromotionID = c.PromotionID,
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                QualityGrade = c.QualityGrade,
                BasePrice = c.BasePrice,
                DiscountPrice = c.DiscountPrice,
                EndDate = c.EndDate,
                StartDate = c.StartDate,
                Stock = c.Stock,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ShopID == ShopID);

            if (_promotionViewModelByShopID == null)
            {
                return NotFound(Responses.NotFound("Promotion"));
            }
            return Ok(_promotionViewModelByShopID.ToList());
        }

        [HttpPost]
        [Route("PromotionAdd")]
        public async Task<ActionResult<PromotionViewModel>> AddPromotion([FromBody] PromotionViewModel promotion)
        {
            if (_context.promotion.Any(c => c.ProductID == promotion.ProductID && c.IsActive == true && c.ShopID == promotion.ShopID))
            {
                return BadRequest(Responses.BadResponse("promotion", "duplicate"));
            }
            if (!_context.shops.Any(c => c.ShopID == promotion.ShopID))
            {
                return BadRequest(Responses.BadResponse("shop", "invalid"));
            }
            var ProductCategoryID = 1;
            ProductViewModel theProduct = new ProductViewModel()
            {
                CreatedDate = (DateTime.Now).ToString(),
                description = promotion.productDescription,
                IsDeleted = promotion.IsDeleted,
                ProductCode = promotion.PromotionID,
                ProductTitle = promotion.ProductTitle,
                ShopID = promotion.ShopID,
                ProductCategoryID = ProductCategoryID,
                ProductID = promotion.ProductID,
            };

            ProductsController productsController = new ProductsController(_context);
            await productsController.AddProduct(theProduct);

            var a = _context.products.FirstOrDefault(c => c.ProductID == promotion.ProductID);
            Promotion thePromotion = new Promotion();
            thePromotion.ProductID = ProductCategoryID;
            thePromotion.ShopID = promotion.ShopID;
            thePromotion.BasePrice = promotion.BasePrice;
            thePromotion.DiscountPrice = promotion.DiscountPrice;
            thePromotion.Stock = promotion.Stock;
            thePromotion.QualityGrade = promotion.QualityGrade;
            thePromotion.StartDate = promotion.StartDate;
            thePromotion.EndDate = promotion.EndDate;
            thePromotion.IsActive = promotion.IsActive;
            thePromotion.IsDeleted = promotion.IsDeleted;
            thePromotion.CreatedDate = (DateTime.Now).ToString();
            thePromotion.productFk = _context.products.FirstOrDefault(c => c.ProductID == promotion.ProductID);
            thePromotion.shopFk = _context.shops.FirstOrDefault(c => c.ShopID == promotion.ShopID);


            _context.promotion.Add(thePromotion);
            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("promotion", "add"));
        }

        //PromotionUpdate: was been commented!
        [HttpPut]
        [Route("PromotionUpdate")]
        public async Task<IActionResult> UpdateProduct([FromBody] PromotionViewModel promotion)
        {
            if (!_context.promotion.Any(c => c.ProductID == promotion.ProductID))
            {
                return NotFound(Responses.NotFound("Promotion"));
            }
            var shopId = _context.shops.FirstOrDefault(c => c.ShopID == promotion.ShopID);
            var proId = _context.products.FirstOrDefault(c => c.ProductID == promotion.ProductID);

            if (shopId == null)
            {
                return BadRequest(Responses.BadResponse("shop", "invalid"));
            }
            if (proId == null)
            {
                return BadRequest(Responses.BadResponse("product", "invalid"));
            }

            Promotion promotionToUpdate = _context.promotion.FirstOrDefault(c => c.PromotionID == promotion.PromotionID);
            promotionToUpdate.ProductID = promotion.ProductID;
            promotionToUpdate.ShopID = promotion.ShopID;
            promotionToUpdate.BasePrice = promotion.BasePrice;
            promotionToUpdate.DiscountPrice = promotion.DiscountPrice;
            promotionToUpdate.Stock = promotion.Stock;
            promotionToUpdate.QualityGrade = promotion.QualityGrade;
            promotionToUpdate.StartDate = promotion.StartDate;
            promotionToUpdate.EndDate = promotion.EndDate;
            promotionToUpdate.IsActive = promotion.IsActive;
            promotionToUpdate.IsDeleted = promotion.IsDeleted;
            promotionToUpdate.CreatedDate = promotion.CreatedDate;
            promotionToUpdate.productFk = _context.products.FirstOrDefault(c => c.ProductID == promotion.ProductID);
            promotionToUpdate.shopFk = _context.shops.FirstOrDefault(c => c.ShopID == promotion.ShopID);


            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("product", "mod"));
        }

        [HttpDelete]
        [Route("PromotionDelete")]
        public async Task<IActionResult> DeletePromotion(long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound(Responses.NotFound("Promotion"));
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsDeleted = true;


            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("promotion", "del"));
        }

        [HttpPost]
        [Route("PromotionDeleteUndo")]
        public async Task<IActionResult> DeletePromotionUndo(long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound(Responses.NotFound("Promotion"));
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsDeleted = false;


            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("promotion", "undel"));
        }

        [HttpPost]
        [Route("PromotionActivate")]
        public async Task<IActionResult> ActivatePromotion(long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound(Responses.NotFound("Promotion"));
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsActive = true;


            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("promotion", "act"));
        }

        [HttpDelete]
        [Route("PromotionInactivate")]
        public async Task<IActionResult> InactivatePromotion(long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound(Responses.NotFound("Promotion"));
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsActive = false;


            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("promotion", "inact"));
        }
    }


    public class imageViewModel
    {
        public IFormFile img { get; set; }

    }
}

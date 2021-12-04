using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tenetApi.Context;
using tenetApi.Model;
using tenetApi.ViewModel;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEnumerable<PromotionViewModel> _promotionViewModel;
        public PromotionController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("PromotionByID")]
        public async Task<ActionResult<IEnumerable<PromotionViewModel>>> GetpromotionsByID([FromBody] long PromotionID)
        {
            _promotionViewModel = _context.promotion.Select(c => new PromotionViewModel()
            {
                PromotionID = c.PromotionID,
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                QualityGrade = c.QualityGrade,
                BasePrice = c.BasePrice,
                DiscountPrice = c.DiscountPrice,
                EndDate = c.EndDate,
                EndTime = c.EndTime,
                Stock = c.Stock,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.PromotionID == PromotionID);

            if (_promotionViewModel == null)
            {
                return NotFound();
            }

            return _promotionViewModel.ToList();

        }
        [HttpPost]
        [Route("promotionByProductID")]
        public async Task<ActionResult<IEnumerable<PromotionViewModel>>> GetpromotionsByProductID([FromBody] long ProductID)
        {
            _promotionViewModel = _context.promotion.Select(c => new PromotionViewModel()
            {
                PromotionID = c.PromotionID,
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                QualityGrade = c.QualityGrade,
                BasePrice = c.BasePrice,
                DiscountPrice = c.DiscountPrice,
                EndDate = c.EndDate,
                EndTime = c.EndTime,
                Stock = c.Stock,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ProductID == ProductID);

            if (_promotionViewModel == null)
            {
                return NotFound();
            }
            return _promotionViewModel.ToList();
        }
        [HttpPost]
        [Route("promotionByShopID")]
        public async Task<ActionResult<IEnumerable<PromotionViewModel>>> GetpromotionsByShopID([FromBody] long ShopID)
        {
            _promotionViewModel = _context.promotion.Select(c => new PromotionViewModel()
            {
                PromotionID = c.PromotionID,
                ProductID = c.ProductID,
                ShopID = c.ShopID,
                QualityGrade = c.QualityGrade,
                BasePrice = c.BasePrice,
                DiscountPrice = c.DiscountPrice,
                EndDate = c.EndDate,
                EndTime = c.EndTime,
                Stock = c.Stock,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted
            }).ToList().Where(c => c.ShopID == ShopID);

            if (_promotionViewModel == null)
            {
                return NotFound();
            }
            return _promotionViewModel.ToList();
        }
        [HttpPost]
        [Route("AddPromotion")]
        public async Task<ActionResult<PromotionViewModel>> AddPromotion([FromBody] PromotionViewModel promotion)
        {
            if (_context.promotion.Any(c => c.ProductID == promotion.ProductID))
            {
                return BadRequest("this product already has a promotion");
            }
            if (!_context.shops.Any(c => c.ShopID == promotion.ShopID))
            {
                return BadRequest("Shop Not found");
            }
            Promotion thePromotion = new Promotion();
            thePromotion.ProductID = promotion.ProductID;
            thePromotion.ShopID = promotion.ShopID;
            thePromotion.BasePrice = promotion.BasePrice;
            thePromotion.DiscountPrice = promotion.DiscountPrice;
            thePromotion.Stock = promotion.Stock;
            thePromotion.QualityGrade = promotion.QualityGrade;
            thePromotion.EndTime = promotion.EndTime;
            thePromotion.EndDate = promotion.EndDate;
            thePromotion.IsActive = promotion.IsActive;
            thePromotion.IsDeleted = promotion.IsDeleted;
            thePromotion.CreatedDate = DateTime.Now;
            thePromotion.productFk = _context.products.FirstOrDefault(c=> c.ProductID == promotion.ProductID);
            thePromotion.shopFk = _context.shops.FirstOrDefault(c=> c.ShopID == promotion.ShopID);


            _context.promotion.Add(thePromotion);
            await _context.SaveChangesAsync();

            return Ok();
        }
        //PromotionUpdate:
        //[HttpPost]
        //[Route("PromotionUpdate")]
        //public async Task<IActionResult> UpdateProduct([FromBody] PromotionViewModel promotion)
        //{
        //    if (!_context.promotion.Any(c => c.ProductID == promotion.ProductID))
        //    {
        //        return NotFound();
        //    }
        //    var shopId = _context.shops.FirstOrDefault(c => c.ShopID == promotion.ShopID);
        //    var proId = _context.products.FirstOrDefault(c => c.ProductID == promotion.ProductID);

        //    if (shopId == null || proId == null)
        //    {
        //        return BadRequest();
        //    }

        //    Promotion promotionToUpdate = _context.promotion.FirstOrDefault(c => c.PromotionID == promotion.PromotionID);
        //    promotionToUpdate.ProductID = promotion.ProductID;
        //    promotionToUpdate.ShopID = promotion.ShopID;
        //    promotionToUpdate.BasePrice = promotion.BasePrice;
        //    promotionToUpdate.DiscountPrice = promotion.DiscountPrice;
        //    promotionToUpdate.Stock = promotion.Stock;
        //    promotionToUpdate.QualityGrade = promotion.QualityGrade;
        //    promotionToUpdate.EndTime = promotion.EndTime;
        //    promotionToUpdate.EndDate = promotion.EndDate;
        //    promotionToUpdate.IsActive = promotion.IsActive;
        //    promotionToUpdate.IsDeleted = promotion.IsDeleted;
        //    promotionToUpdate.CreatedDate = promotion.CreatedDate;
        //    promotionToUpdate.productFk = _context.products.FirstOrDefault(c => c.ProductID == promotion.ProductID);
        //    promotionToUpdate.shopFk = _context.shops.FirstOrDefault(c => c.ShopID == promotion.ShopID);


        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
        [HttpPost]
        [Route("PromotionDelete")]
        public async Task<IActionResult> DeletePromotion([FromBody] long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound();
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsDeleted = true;


            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost]
        [Route("PromotionDeleteUndo")]
        public async Task<IActionResult> DeletePromotionUndo([FromBody] long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound();
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsDeleted = false;


            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost]
        [Route("PromotionActivate")]
        public async Task<IActionResult> ActivatePromotion([FromBody] long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound();
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsActive = true;


            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost]
        [Route("PromotionInactivate")]
        public async Task<IActionResult> InactivatePromotion([FromBody] long promotionId)
        {

            if (!_context.promotion.Any(c => c.PromotionID == promotionId))
            {
                return NotFound();
            }

            Promotion productToDelete = _context.promotion.FirstOrDefault(c => c.PromotionID == promotionId);
            productToDelete.IsActive = false;


            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

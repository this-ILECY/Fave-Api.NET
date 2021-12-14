using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tenetApi.Context;
using tenetApi.Exception;
using tenetApi.Model;
using tenetApi.ViewModel;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy ="Shop", AuthenticationSchemes = "Bearer")]
    public class ShopController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ShopByID")]
        public async Task<ActionResult<ShopViewModel>> GetShopByID(long ShopID)
        {
            ShopViewModel _shopViewModelByID;
            _shopViewModelByID = _context.shops.Select(c => new ShopViewModel()
            {
                ShopID = c.ShopID,
                UserID = c.UserID,
                ShopCategoryID = c.ShopCategoryID,
                ShopName = c.ShopName,
                ShopAddress = c.ShopAddress,
                TelePhone = c.TelePhone,
                CellPhone = c.CellPhone,
                ShopLatitude = c.ShopLatitude,
                ShopLongitude = c.ShopLongitude,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted,
                CreatedDate = c.CreatedDate

            }).ToList().FirstOrDefault(c => c.ShopID == ShopID);

            if (_shopViewModelByID == null)
            {
                return NotFound(Responses.NotFound("Shop"));
            }

            return Ok(_shopViewModelByID);
        }

        [HttpGet]
        [Route("ShopByName")]
        public async Task<ActionResult<IEnumerable<ShopViewModel>>> GetShopByName(string ShopName)
        {
            IEnumerable<ShopViewModel> _shopViewModelByName;
            ShopName = ShopName.ToLower();
            if (ShopName.Contains(" "))
            {
                ShopName = ShopName.Replace(" ", "_");
            }
            _shopViewModelByName = _context.shops.Select(c => new ShopViewModel()
            {
                ShopID = c.ShopID,
                UserID = c.UserID,
                ShopCategoryID = c.ShopCategoryID,
                ShopName = c.ShopName,
                ShopAddress = c.ShopAddress,
                TelePhone = c.TelePhone,
                CellPhone = c.CellPhone,
                ShopLatitude = c.ShopLatitude,
                ShopLongitude = c.ShopLongitude,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted,
                CreatedDate = c.CreatedDate

            }).ToList().Where(c => c.ShopName.Contains(ShopName));

            if (_shopViewModelByName == null)
            {
                return NotFound(Responses.NotFound("Shop"));
            }

            return Ok(_shopViewModelByName.ToList());
        }

        [HttpGet]
        [Route("ShopByAddress")]
        public async Task<ActionResult<IEnumerable<ShopViewModel>>> GetShopByAddress(string ShopAddress)
        {
            IEnumerable<ShopViewModel> _shopViewModelByAddress;
            ShopAddress = ShopAddress.ToLower();

            _shopViewModelByAddress = _context.shops.Select(c => new ShopViewModel()
            {
                ShopID = c.ShopID,
                UserID = c.UserID,
                ShopCategoryID = c.ShopCategoryID,
                ShopName = c.ShopName,
                ShopAddress = c.ShopAddress,
                TelePhone = c.TelePhone,
                CellPhone = c.CellPhone,
                ShopLatitude = c.ShopLatitude,
                ShopLongitude = c.ShopLongitude,
                IsActive = c.IsActive,
                IsDeleted = c.IsDeleted,
                CreatedDate = c.CreatedDate

            }).ToList().Where(c => c.ShopAddress.Contains(ShopAddress));

            if (_shopViewModelByAddress == null)
            {
                return NotFound(Responses.NotFound("Shop"));
            }

            return Ok(_shopViewModelByAddress.ToList());
        }

        [HttpPost]
        [Route("AddShop")]
        public async Task<ActionResult<ShopViewModel>> AddShop([FromBody] ShopViewModel shop)
        {
            
            shop.ShopName = shop.ShopName.ToLower();
            if (shop.ShopName.Contains(" "))
            {
                shop.ShopName = shop.ShopName.Replace(" ", "_");
            }
            if (_context.shops.Any(c => c.ShopName == shop.ShopName))
            {
                return BadRequest(Responses.BadResponde("Shop", "invalid"));
            }
            if (_context.shops.Any(c => c.UserID == shop.UserID))
            {
                return BadRequest(Responses.BadResponde("User", "invalid"));
            }
            if (!_context.shops.Any(c => c.ShopCategoryID == shop.ShopCategoryID))
            {
                return BadRequest(Responses.BadResponde("Shop Category", "invalid"));
            }

            Shop theShop = new Shop()
            {
                ShopID = shop.ShopID,
                ShopName = shop.ShopName,
                IsActive = shop.IsActive,
                CreatedDate = DateTime.Now,
                ShopCategoryID = shop.ShopCategoryID,
                CellPhone = shop.CellPhone,
                IsDeleted = shop.IsDeleted,
                ShopAddress = shop.ShopAddress,
                ShopLatitude = shop.ShopLatitude,
                ShopLongitude = shop.ShopLongitude,
                TelePhone = shop.TelePhone,
                UserID = shop.UserID,
                shopCategoryFk = _context.shopCategories.FirstOrDefault(c => c.ShopCategoryID == shop.ShopCategoryID),
                userFk = _context.Users.FirstOrDefault(c => c.Id == shop.UserID)
            };

            _context.shops.Add(theShop);
            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("Shop", "add"));
        }

        [HttpPut]
        [Route("UpdateShop")]
        public async Task<ActionResult<ShopViewModel>> UpdateShop([FromBody] ShopViewModel shop)
        {
            shop.ShopName = shop.ShopName.ToLower();
            if (shop.ShopName.Contains(" "))
            {
                shop.ShopName = shop.ShopName.Replace(" ", "_");
            }
            if (!_context.shops.Any(c => c.ShopName == shop.ShopName))
            {
                return BadRequest(Responses.BadResponde("Shop", "invalid"));
            }
            if (!_context.shops.Any(c => c.UserID == shop.UserID))
            {
                return BadRequest(Responses.BadResponde("User", "invalid"));
            }
            if (!_context.shops.Any(c => c.ShopCategoryID == shop.ShopCategoryID))
            {
                return BadRequest(Responses.BadResponde("Shop category", "invalid"));
            }


            Shop shopToUpdate = _context.shops.FirstOrDefault(c => c.ShopID == shop.ShopID);

            shopToUpdate.IsActive = shop.IsActive;
            shopToUpdate.ShopName = shop.ShopName;
            shopToUpdate.CreatedDate = shop.CreatedDate;
            shopToUpdate.ShopCategoryID = shop.ShopCategoryID;
            shopToUpdate.CellPhone = shop.CellPhone;
            shopToUpdate.IsDeleted = shop.IsDeleted;
            shopToUpdate.ShopAddress = shop.ShopAddress;
            shopToUpdate.ShopLatitude = shop.ShopLatitude;
            shopToUpdate.ShopLongitude = shop.ShopLongitude;
            shopToUpdate.TelePhone = shop.TelePhone;
            shopToUpdate.UserID = shop.UserID;
            shopToUpdate.shopCategoryFk = _context.shopCategories.FirstOrDefault(c => c.ShopCategoryID == shop.ShopCategoryID);
            shopToUpdate.userFk = _context.Users.FirstOrDefault(c => c.Id == shop.UserID);


            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("Shop", "mod"));
        }

        [HttpDelete]
        [Route("ShopDelete")]
        public async Task<IActionResult> DeleteShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound(Responses.NotFound("Shop"));
            }

            Shop shopToDelete = await _context.shops.FirstOrDefaultAsync(c => c.ShopID == shopId);
            shopToDelete.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("Shop", "del"));
        }

        [HttpPost]
        [Route("ShopDeleteUndo")]
        public async Task<IActionResult> UndoDeleteShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound(Responses.NotFound("Shop"));
            }

            Shop shopToDelete = _context.shops.FirstOrDefault(c => c.ShopID == shopId);
            shopToDelete.IsDeleted = false;

            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("Shop", "undel"));
        }

        [HttpDelete]
        [Route("ShopInactivate")]
        public async Task<IActionResult> InactivateShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound(Responses.NotFound("Shop"));
            }

            Shop shopToDelete = await _context.shops.FirstOrDefaultAsync(c => c.ShopID == shopId);
            shopToDelete.IsActive = false;

            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("Shop", "inact"));
        }

        [HttpPost]
        [Route("ShopActivate")]
        public async Task<IActionResult> ActivateShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound(Responses.NotFound("Shop"));
            }

            Shop shopToDelete = await _context.shops.FirstOrDefaultAsync(c => c.ShopID == shopId);
            shopToDelete.IsActive = true;

            await _context.SaveChangesAsync();

            return Ok(Responses.OkResponse("Shop", "act"));
        }
    }
}

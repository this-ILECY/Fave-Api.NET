using Microsoft.AspNetCore.Mvc;
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
    public class ShopController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEnumerable<ShopViewModel> _shopViewModel;

        public ShopController(AppDbContext context)
        {
                _context = context;
        }

        [HttpGet]
        [Route("ShopByID")]
        public async Task<ActionResult<IEnumerable<ShopViewModel>>> GetShopByID(long ShopID)
        {
            _shopViewModel = _context.shops.Select(c => new ShopViewModel()
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
                
            }).ToList().Where(c=> c.ShopID == ShopID);

            if (_shopViewModel == null)
            {
                return NotFound();
            }

            return _shopViewModel.ToList();
        }

        [HttpGet]
        [Route("ShopByName")]
        public async Task<ActionResult<IEnumerable<ShopViewModel>>> GetShopByName(string ShopName)
        {
            ShopName = ShopName.ToLower();
            if(ShopName.Contains(" "))
            {
                ShopName = ShopName.Replace(" ","_");
            }
            _shopViewModel = _context.shops.Select(c => new ShopViewModel()
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

            if (_shopViewModel == null)
            {
                return NotFound();
            }

            return _shopViewModel.ToList();
        }

        [HttpGet]
        [Route("ShopByAddress")]
        public async Task<ActionResult<IEnumerable<ShopViewModel>>> GetShopByAddress(string ShopAddress)
        {
            ShopAddress = ShopAddress.ToLower();
            
            _shopViewModel = _context.shops.Select(c => new ShopViewModel()
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

            if (_shopViewModel == null)
            {
                return NotFound();
            }

            return _shopViewModel.ToList();
        }

        [HttpPost]
        [Route("AddShop")]
        public async Task<ActionResult<ShopViewModel>> AddShop([FromBody] ShopViewModel shop)
        {
            shop.ShopName = shop.ShopName.ToLower();
            if(shop.ShopName.Contains(" "))
            {
                shop.ShopName = shop.ShopName.Replace(" ","_");
            }
            if (_context.shops.Any(c => c.ShopName == shop.ShopName))
            {
                return BadRequest("Shop name unavailable!");
            }
            if(_context.shops.Any(c=> c.UserID == shop.UserID))
            {
                return BadRequest("Invalid User ID");
            }
            if (!_context.shops.Any(c=> c.ShopCategoryID == shop.ShopCategoryID))
            {
                return BadRequest("Invalid ShopCategory");
            }

            try
            {

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

            }
            catch (System.Exception e)
            {
                var msg = Log.Error(e.Message);
                return BadRequest();
            }
            return Ok();
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
                return BadRequest("Name Unavailable!");
            }
            if (!_context.shops.Any(c => c.UserID == shop.UserID))
            {
                return BadRequest("Invalid userID");
            }
            if (!_context.shops.Any(c => c.ShopCategoryID == shop.ShopCategoryID))
            {
                return BadRequest("Invalid Shop Category!");
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

            return Ok();
        }
        
        [HttpDelete]
        [Route("ShopDelete")]
        public async Task<IActionResult> DeleteShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound();
            }

            Shop shopToDelete = _context.shops.FirstOrDefault(c => c.ShopID == shopId);
            shopToDelete.IsDeleted = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPost]
        [Route("ShopDeleteUndo")]
        public async Task<IActionResult> UndoDeleteShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound();
            }

            Shop shopToDelete = _context.shops.FirstOrDefault(c => c.ShopID == shopId);
            shopToDelete.IsDeleted = false;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpDelete]
        [Route("ShopInactivate")]
        public async Task<IActionResult> InactivateShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound();
            }

            Shop shopToDelete = _context.shops.FirstOrDefault(c => c.ShopID == shopId);
            shopToDelete.IsActive = false;

            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPost]
        [Route("ShopActivate")]
        public async Task<IActionResult> ActivateShop(long shopId)
        {

            if (!_context.shops.Any(c => c.ShopID == shopId))
            {
                return NotFound();
            }

            Shop shopToDelete = _context.shops.FirstOrDefault(c => c.ShopID == shopId);
            shopToDelete.IsActive = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

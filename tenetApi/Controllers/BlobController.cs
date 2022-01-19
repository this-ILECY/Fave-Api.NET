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
    public class BlobController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BlobController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("getavatar")]
        public async Task<ActionResult<Shop>> Getavatar(IList<IFormFile> avatar)
        {
            if (avatar[0].Length > 500000)
            {
                return BadRequest(Responses.BadResponse("blob", "heavy"));
            }
            string avatarPath;
            string AvatarOldName = avatar[0].FileName;
            string AvatarName = AvatarOldName + Guid.NewGuid().ToString();

            var AvatarExtension = "." + avatar[0].ContentType.Substring(avatar[0].ContentType.LastIndexOf("/") + 1);

            if (!avatar[0].ContentType.Contains("image"))
            {
                return BadRequest(Responses.BadResponse("blob", "extension"));
            }
            string avatarfile = AvatarName + AvatarExtension;
        again:
            if (Directory.Exists("~/../img/avatar"))
            {

                if (AvatarOldName.Contains("shop"))
                {
                    if (!Directory.Exists("~/../img/avatar/shop"))
                    {
                        Directory.CreateDirectory("~/../img/avatar/shop");
                    }
                    avatarPath = Path.Combine("~/../img/avatar/shop", avatarfile);
                }
                else if (AvatarOldName.Contains("customer"))
                {
                    if (!Directory.Exists("~/../img/avatar/customer"))
                    {
                        Directory.CreateDirectory("~/../img/avatar/customer");
                    }
                    avatarPath = Path.Combine("~/../img/avatar/customer", avatarfile);
                }
                else
                {
                    return BadRequest(Responses.BadResponse("blob", "userType"));
                }

                using (Stream fileStream = new FileStream(avatarPath, FileMode.Create))
                {
                    await avatar[0].CopyToAsync(fileStream);
                }
            }
            else
            {
                Directory.CreateDirectory("~/../img/avatar");
                goto again;
            }
            string avatarFullPath = Path.GetFullPath(avatarPath);
            return Ok(avatarFullPath);

        }

    
    [HttpPost]
    [Route("getbanner")]
    public async Task<ActionResult<Shop>> Getbanner(IList<IFormFile> banner)
    {
        if (banner[0].Length > 700000)
        {
            return BadRequest(Responses.BadResponse("blob", "heavy"));
        }

        string avatarPath;
        string AvatarOldName = banner[0].FileName;
        string AvatarName = AvatarOldName + Guid.NewGuid().ToString();

        var AvatarExtension = "." + banner[0].ContentType.Substring(banner[0].ContentType.LastIndexOf("/") + 1);

        if (!banner[0].ContentType.Contains("image"))
        {
            return BadRequest(Responses.BadResponse("blob", "extension"));
        }
        string avatarfile = AvatarName + AvatarExtension;
    again:
        if (Directory.Exists("~/../img/banner"))
        {

            if (AvatarOldName.Contains("shop"))
            {
                if (!Directory.Exists("~/../img/banner/shop"))
                {
                    Directory.CreateDirectory("~/../img/banner/shop");
                }
                avatarPath = Path.Combine("~/../img/banner/shop", avatarfile);
            }
            else if (AvatarOldName.Contains("customer"))
            {
                if (!Directory.Exists("~/../img/banner/customer"))
                {
                    Directory.CreateDirectory("~/../img/banner/customer");
                }
                avatarPath = Path.Combine("~/../img/banner/customer", avatarfile);
            }
            else
            {
                return BadRequest(Responses.BadResponse("blob", "userType"));
            }

            using (Stream fileStream = new FileStream(avatarPath, FileMode.Create))
            {
                await banner[0].CopyToAsync(fileStream);
            }
        }
        else
        {
            Directory.CreateDirectory("~/../img/banner");
            goto again;
        }
        string avatarFullPath = Path.GetFullPath(avatarPath);
        return Ok(avatarFullPath);

    }

}
}


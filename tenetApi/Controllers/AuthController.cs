using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using tenetApi.Exception;
using tenetApi.Model;
using System.Net.Mail;
using System.Text.RegularExpressions;
using tenetApi.ViewModel;
using tenetApi.Context;

namespace tenetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration,
            AppDbContext context)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("UserCheck")]
        public async Task<IActionResult> UserCheck([FromHeader] String username)
        {
            if (username == null)
                username = " ";
            if (username.Contains(" "))
                username = username.Replace(" ", "_").ToLower();


            var UserExists = await userManager.FindByNameAsync(username);

            if (UserExists != null)
                return BadRequest(Responses.BadResponse("user", "duplicate"));
            else
                return Ok("found!");
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] SignInUpModel login)
        {
            if (login.UserName.Contains(" "))
                login.UserName = login.UserName.Replace(" ", "_").ToLower();
            if (login.Email == null)
                login.Email = "";
            if (login.Phone == null)
                login.Phone = "";

            var userNameExists = await userManager.FindByNameAsync(login.UserName);
            var userEmailExists = await userManager.FindByNameAsync(login.Email);
            var userPhoneExists = await userManager.FindByNameAsync(login.Phone);
            var roleExists = await roleManager.FindByNameAsync(login.Role);
            if (userNameExists != null)
                return BadRequest(Responses.BadResponse("user", "duplicate"));
            if (userEmailExists != null)
                return BadRequest(Responses.BadResponse("email", "duplicate"));
            if (userPhoneExists != null)
                return BadRequest(Responses.BadResponse("phone", "duplicate"));
            if (roleExists == null)
                return BadRequest(Responses.BadResponse("role", "invalid"));


            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = login.UserName,
                Email = login.Email,
                PhoneNumber = login.Phone
            };
            var result = await userManager.CreateAsync(user, login.UserPassword);

            if (result.Succeeded)
            {
                var role = await userManager.AddToRoleAsync(user, login.Role);
                if (!role.Succeeded == true)
                    return BadRequest(Responses.BadResponse("user", "invalid") + " " + result);

                ShopViewModel shopmodel = new ShopViewModel()
                {
                    UserID = user.Id,
                    ShopCategoryID = 2,
                    ShopName = user.UserName,
                    ShopAddress = "",
                    TelePhone = "",
                    CellPhone = "",
                    ShopAvatar = "",
                    ShopBanner = "",
                    ShopLatitude = 0,
                    ShopLongitude = 0,
                    IsActive = true
                };

                ShopController shop = new ShopController(_context);
                await shop.AddShop(shopmodel);

                var loginContent = await Login(login);

                return Ok(loginContent);

            }
            else
                return BadRequest(Responses.BadResponse("user", "invalid") + " " + result);

        }


        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] SignInUpModel login)
        {
            if (login.UserName.Contains(" "))
            {
                login.UserName = login.UserName.Replace(" ", "_").ToLower();
            }
            var role = await roleManager.RoleExistsAsync(login.Role);
            var emailformat = Regex.IsMatch(login.UserName, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            var phoneformat = Regex.IsMatch(login.UserName, @"^[1-9]\d{11}$");
            User user = new User();
            if (emailformat)
            {
                user = await userManager.FindByEmailAsync(login.UserName);
            }
            else if (phoneformat)
            {
                user = await userManager.FindByEmailAsync(login.UserName);
            }
            else
            {
                user = await userManager.FindByNameAsync(login.UserName);

            }

            var xx = await userManager.CheckPasswordAsync(user, login.UserPassword);

            if (user == null || !xx)
            {
                return BadRequest(Responses.BadResponse("login credentials", "invalid"));
            }
            else
            {
                //}
                //if (role == true && user != null)
                //{
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                expires: DateTime.UtcNow.AddMinutes(30),
                audience: _configuration["JWT:Audience"],
                claims: authClaims,


                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = user.UserName
                });
            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("refreshToken")]

        public async Task<IActionResult> RefreshToken([FromHeader] string token)
        {
            if (token == null || token == "")
                return Unauthorized(Responses.Unathorized("token", "bad"));

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var validateToken = jwtSecurityTokenHandler.CanReadToken(token);
            if (!validateToken)
                return Unauthorized(Responses.Unathorized("token", "bad"));

            var jsonToken = jwtSecurityTokenHandler.ReadToken(token.ToString());
            var tokenS = jsonToken as JwtSecurityToken;

            var name = tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var role = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            var tokenTime = tokenS.ValidTo;
            var date = new DateTime();
            date = DateTime.Now;
            tokenTime = tokenTime.ToLocalTime();

            if (date > tokenTime)
            {
                return Unauthorized(Responses.Unathorized("token", "expired"));
            }
            else
            {
                var userExists = userManager.FindByNameAsync(name).Result;
                var roleExists = roleManager.FindByNameAsync(role).Result;

                if (userExists != null && roleExists != null)
                {
                    var userRoles = await userManager.GetRolesAsync(userExists);
                    var authClaims = new List<Claim>{
                        new Claim(ClaimTypes.Name, userExists.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    var newToken = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    expires: DateTime.UtcNow.AddMinutes(30),
                    audience: _configuration["JWT:Audience"],
                    claims: authClaims,


                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(newToken),
                        expiration = newToken.ValidTo,
                        user = userExists.UserName
                    });
                }

                return Unauthorized(Responses.Unathorized("token", "bad"));
            }
        }

    }
}

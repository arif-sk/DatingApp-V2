using DatingApp_API.Data;
using DatingApp_API.DTOs;
using DatingApp_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp_API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext context;

        public AccountController(DataContext context)
        {
            this.context = context;
        }
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> RegisterUser(RegisterDto register)
        {
            if (await UserExists(register.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                UserName = register.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto login)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower().Equals(login.Username.ToLower()));
            if (user == null) return Unauthorized("Invalid User");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for(var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return user;
        }
        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}

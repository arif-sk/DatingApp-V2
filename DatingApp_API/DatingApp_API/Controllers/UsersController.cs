using DatingApp_API.Data;
using DatingApp_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            HttpContext.Session.SetString("IsStopped", "true");

            return await _context.Users.ToListAsync();
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var hasStopped = HttpContext.Session.GetString("IsStopped");

            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

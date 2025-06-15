using AvinashBackEndAPI.Data;
using AvinashBackEndAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvinashBackEndAPI.Controllers
{
    [Route("api/[controller]")] // This will resolve to api/UserManager
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserManagerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(string? lastname = null, string? id = null)
        {
            IQueryable<Register> query = _context.Registrations;

            if (!string.IsNullOrEmpty(id))
            {
                return Ok(await query.Where(u => u.Id.ToString() == id).ToListAsync());
            }

            if (!string.IsNullOrEmpty(lastname))
            {
                if (lastname == "*")
                    return Ok(await query.ToListAsync());

                return Ok(await query.Where(u => u.LastName.ToLower().Contains(lastname.ToLower())).ToListAsync());
            }

            return BadRequest("Please provide either lastname or userId.");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using AvinashBackEndAPI.Data;
using AvinashBackEndAPI.Models;

namespace AvinashBackEndAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Registrations.Add(model);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully", data = model });
        }
    }
}

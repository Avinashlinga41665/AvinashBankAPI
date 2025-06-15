using Microsoft.AspNetCore.Mvc;
using AvinashBackEndAPI.Data;
using AvinashBackEndAPI.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Register([FromBody] Register registration)
        {
            // Convert DateOfBirth to UTC if it's not already
            if (registration.DateOfBirth.Kind == DateTimeKind.Unspecified)
            {
                registration.DateOfBirth = DateTime.SpecifyKind(registration.DateOfBirth, DateTimeKind.Local).ToUniversalTime();
            }
            else
            {
                registration.DateOfBirth = registration.DateOfBirth.ToUniversalTime();
            }

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful", registration.Id });
        }
    }
}

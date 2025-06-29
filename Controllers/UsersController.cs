namespace AvinashBackEndAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq;
    using BCrypt.Net; 
    using global::AvinashBackEndAPI.Data;
    using global::AvinashBackEndAPI.Models;
    using global::AvinashBackEndAPI.DTO;

    namespace AvinashBackEndAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class UsersController : ControllerBase
        {
            private readonly AppDbContext _context;

            public UsersController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
            {
                var user = new User
                {
                    AadharNumber = dto.AadharNumber,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    MobileNumber = dto.MobileNumber,
                    Email = dto.Email,
                    LoginID = dto.LoginID, 
                    DateOfBirth = dto.DateOfBirth.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(dto.DateOfBirth, DateTimeKind.Local).ToUniversalTime()
                        : dto.DateOfBirth.ToUniversalTime(),
                    PasswordHash = BCrypt.HashPassword(dto.Password)
                };
                if (_context.Users.Any(u => u.LoginID == dto.LoginID))
                {
                    return BadRequest(new { message = "LoginID already exists." });
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Registration successful", user.Id });
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginDTO loginRequest)
            {
                var user = _context.Users.FirstOrDefault(u => u.LoginID == loginRequest.LoginID);


                if (user == null)
                    return Unauthorized(new { message = "Invalid credentials" });

                bool isPasswordValid = BCrypt.Verify(loginRequest.Password, user.PasswordHash);

                if (!isPasswordValid)
                    return Unauthorized(new { message = "Invalid credentials" });

                return Ok(new { message = "Login successful" });
            }

        }
    }

}

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
                    AccountNumber = dto.AccountNumber,
                    AccountType = dto.AccountType,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    MobileNumber = dto.MobileNumber,
                    Email = dto.Email,
                    LoginName = dto.LoginName, 
                    DateOfBirth = dto.DateOfBirth.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(dto.DateOfBirth, DateTimeKind.Local).ToUniversalTime()
                        : dto.DateOfBirth.ToUniversalTime(),
                    PasswordHash = BCrypt.HashPassword(dto.Password)
                };
                if (_context.Users.Any(u => u.LoginName == dto.LoginName))
                {
                    return BadRequest(new { message = "LoginID already exists." });
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                await DummyAccountSummaryData(user.Id, dto.AccountNumber, dto.AccountType);

                return Ok(new { message = "Registration successful", user.Id });
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginDTO loginRequest)
            {
                var user = _context.Users.FirstOrDefault(u => u.LoginName == loginRequest.LoginName);


                if (user == null)
                    return Unauthorized(new { message = "Invalid credentials" });

                bool isPasswordValid = BCrypt.Verify(loginRequest.Password, user.PasswordHash);

                if (!isPasswordValid)
                    return Unauthorized(new { message = "Invalid credentials" });

                var accounts = _context.Accounts.Where(a => a.UserID == user.Id).Select(a => new {
                                                                                a.AccountNumber,
                                                                                a.AccountType,
                                                                                a.Balance,
                                                                                a.Status
                                                                                }).ToList();

                return Ok(new { message = "Login successful",
                    userId = user.Id,
                    loginName = user.LoginName,
                    lastName = user.LastName,
                    accounts = accounts

                });
            }
            [NonAction]
            public async Task DummyAccountSummaryData(int id, string accountNumber,string accountType)
            {
                var DummyData = new Account
                {
                    UserID = id,
                    AccountNumber = accountNumber,
                    AccountType = accountType,
                    Balance = 3000,
                    Status = true
                };
                _context.Accounts.Add(DummyData);
                await _context.SaveChangesAsync();
            }

        }
    }

}

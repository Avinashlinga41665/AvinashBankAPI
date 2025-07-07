namespace AvinashBackEndAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq;
    using BCrypt.Net; 
    using global::AvinashBackEndAPI.Data;
    using global::AvinashBackEndAPI.Models;
    using global::AvinashBackEndAPI.DTO;
    using Microsoft.EntityFrameworkCore;

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

                var accounts = _context.Accounts.Where(a => a.UserId == user.Id).Select(a => new {
                                                                                a.AccountNumber,
                                                                                a.AccountType,
                                                                                a.Balance,
                                                                                a.Status
                                                                                }).ToList();

                var transactions = _context.Transactions
                       .Where(t => t.DebitAccount.UserId == user.Id || t.CreditAccount.UserId == user.Id)
                       .Select(t => new {
                           t.TimeOfTransfer,
                           t.TypeOfPayment,
                           t.Description,
                           t.Amount,
                           t.TransactionStatus
                       })
                       .ToList();

                return Ok(new { message = "Login successful",
                    userId = user.Id,
                    loginName = user.LoginName,
                    lastName = user.LastName,
                    accounts = accounts,
                    transactions = transactions

                });
            }
            [HttpPost("transfer")]
            public async Task<IActionResult> Transfer([FromBody] MakeaTransferDTO dto)
            {
                if (dto.Amount <= 0)
                    return BadRequest("Amount must be greater than zero.");

                var fromAcc = _context.Accounts.FirstOrDefault(a => a.AccountNumber == dto.FromAccountNumber);
                var toAcc = _context.Accounts.FirstOrDefault(a => a.AccountNumber == dto.ToAccountNumber);

                if (fromAcc == null || toAcc == null)
                    return NotFound("One or both accounts not found.");

                if (fromAcc.Balance < dto.Amount)
                    return BadRequest("Insufficient funds.");

                var allowed = _context.TransferTable.FirstOrDefault(t =>
                    t.fromAccountName == fromAcc.AccountType &&
                    t.toAccountName == toAcc.AccountType &&
                    t.status == true);

                if (allowed == null)
                    return BadRequest("Transfer not allowed for this account combination.");

                fromAcc.Balance -= dto.Amount;
                toAcc.Balance += dto.Amount;

                var transaction = new Transaction
                {
                    TransferID = Guid.NewGuid().ToString(),
                    UserId = fromAcc.UserId,
                    TypeOfPayment = dto.TransferType,
                    Amount = dto.Amount,
                    TimeOfTransfer = DateTime.UtcNow,
                    TransactionStatus = true,
                    Description = $"Transfer from {dto.FromAccountNumber} to {dto.ToAccountNumber}",
                    DebitAccountId = fromAcc.Id,  
                    CreditAccountId = toAcc.Id
                };

                _context.Transactions.Add(transaction);

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Transfer successful",
                    transactionId = transaction.TransferID
                });
            }
            [NonAction]
            public async Task DummyAccountSummaryData(int id, string accountNumber,string accountType)
            {
                var DummyData1 = new Account
                {
                    UserId = id,
                    AccountNumber = accountNumber,
                    AccountType = accountType,
                    Balance = 3000,
                    Status = true
                };
                var DummyData2 = new Account
                {
                    UserId = id,
                    AccountNumber = "1235647839",
                    AccountType = "DDA",
                    Balance = 3000,
                    Status = true
                };
                var DummyData3 = new Account
                {
                    UserId = id,
                    AccountNumber = "0987654321",
                    AccountType = "LAS",
                    Balance = 30000,
                    Status = true
                };
                _context.Accounts.Add(DummyData1);
                _context.Accounts.Add(DummyData2);
                _context.Accounts.Add(DummyData3);
                await _context.SaveChangesAsync();
            }

        }
    }

}

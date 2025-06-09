using AvinashBackEndAPI.Data;
using AvinashBackEndAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Login([FromBody] Login login)
    {
        var user = _context.Users.FirstOrDefault(u =>
            u.Userloginname == login.Userloginname && u.Userloginpwd == login.Userloginpwd);

        if (user == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new { message = "Login successful" });
    }
    [HttpGet("all")]
    public IActionResult GetAllUsers([FromServices] AppDbContext context)
    {
        var users = context.Users.ToList();
        return Ok(users);
    }
}

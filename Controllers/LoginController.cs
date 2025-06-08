using Microsoft.AspNetCore.Mvc;
using AvinashBackEndAPI.Models;

namespace AvinashBackEndAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            var validUsername = "avinash";
            var validPassword = "1234567";

            if (login.Userloginname == validUsername && login.Userloginpwd == validPassword)
            {
                return Ok(new { message = "Login successful" });
            }
            else
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
        }
    }
}

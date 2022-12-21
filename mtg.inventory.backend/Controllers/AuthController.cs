using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DefaultDBContext _context;

        public AuthController(DefaultDBContext context)
        {
            _context = context;
        }

        [HttpGet("check-login")]
        public ActionResult CheckLogin()
        {
            return Ok(new
            {
                Status = CheckAuth(HttpContext)
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult> DoLogin(AuthRequest authRequest)
        {
            var user = await _context.User.FirstOrDefaultAsync(user => string.Equals(user.Email, authRequest.Username, StringComparison.OrdinalIgnoreCase));
            
            if (user is null)
            {
                return NotFound(new
                {
                    Status = false,
                    Error = "user-not-found"
                });
            }
                if (user.CheckPassword(authRequest.Password))
                {

                    //Yes let's reinvent the wheel... (btw storing token in session is storing it server side instead of client side, which would be common practice)
                    //ToDo actual JWT implementation

                    // Generate Random Token
                    var sb = new StringBuilder();  
                    var random = new Random();  

                    char letter;  

                    for (int i = 0; i < 32; i++)
                    {
                        double flt = random.NextDouble();
                        int shift = Convert.ToInt32(Math.Floor(25 * flt));
                        letter = Convert.ToChar(shift + 65);
                        sb.Append(letter);  
                    } 
                    
                    HttpContext.Session.SetString("auth-token", sb.ToString());

                    return Ok(new
                    {
                        Status = true,
                        Token = sb.ToString(),
                    });
                }
            
            return Unauthorized(new 
            {
                Status = false, 
                Error = "auth-failed"
            });
        }

        public static bool CheckAuth(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authentication", out var headerTokenOut);

            var headerToken = headerTokenOut[0];
            var sessionToken = context.Session.GetString("auth-token");
            return string.Equals(sessionToken, headerToken, StringComparison.OrdinalIgnoreCase);
        }

    }
}

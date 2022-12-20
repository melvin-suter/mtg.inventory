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
        public async Task<ActionResult> checkLogin(){
            return StatusCode(200,new {status = checkAuth(HttpContext)});
        }

        [HttpPost("login")]
        public async Task<ActionResult> doLogin(AuthRequest authRequest){

            if (_context.User == null)
            {
                return NotFound();
            }
            var user = _context.User.AsEnumerable<User>().Where((User user, int bol) => user.email.ToLower() == authRequest.username.ToLower()).First();
            
            if (user == null){
                if(user.checkPassword(authRequest.password)){
                    // Generate Random Token
                    StringBuilder str_build = new StringBuilder();  
                    Random random = new Random();  

                    char letter;  

                    for (int i = 0; i < 32; i++)
                    {
                        double flt = random.NextDouble();
                        int shift = Convert.ToInt32(Math.Floor(25 * flt));
                        letter = Convert.ToChar(shift + 65);
                        str_build.Append(letter);  
                    } 
                    
                    HttpContext.Session.SetString("auth-token", str_build.ToString());
                    return StatusCode(200,new {status = true, token = str_build.ToString()});
                }
            }                
            
            return StatusCode(200,new {status = false, error = "auth-failed"});
        }

        public static bool checkAuth(HttpContext _context){
            _context.Request.Headers.TryGetValue("Authentication",out StringValues headerTokenOut);
            var headerToken = headerTokenOut.First();
            string sessionToken = _context.Session.GetString("auth-token");
            if(headerToken ==  null|| headerToken.Length <= 0 || sessionToken == null || sessionToken.Length <= 0) { return false;}

            return sessionToken.ToLower() == headerToken.ToLower();
        }

    }
}

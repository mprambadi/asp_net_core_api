using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using core_api.Model;
using core_api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace core_api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {

        private readonly BlogContext _context;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, BlogContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Users.Include(user => user.Posts));
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var client = new HttpClient();

            var user  = _context.Users.FirstOrDefault(e => e.id==id);
            
            if(user==null)
            {
                return NotFound(new {status= HttpStatusCode.NotFound, message="User not found"});
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult AddUser()
        {
            var userData = new User{
                email="john@pantau.com",
                profile="hello world",
                username="johni", 
                Posts= new List<Post>{ 
                    new Post{title="hello world"}
                    }
                };

            _context.Users.Add(userData);
            _context.SaveChanges();
            return Ok(userData);
        }

        [HttpPut("{test}")]
        public int EditUser(User user, int test)
        {
            return test;
        }

        [HttpDelete("{id}")]
        public int DeleteUser(int id)
        {
            return id;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Authenticate(User user)
        {

            var _user = _context.Users.SingleOrDefault(e => e.id==user.id);

            if(_user == null)
            {
                 return BadRequest(new { message = "Username or password is incorrect" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, _user.id.ToString()),
                    new Claim(ClaimTypes.Email, _user.email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secret key nya coy")), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userNew = new {
                    email = _user.email,
                    id = _user.id,
                    username = _user.username,
                    token = tokenHandler.WriteToken(token)
                }; 
            
            return Ok(userNew);
        }

    }
}

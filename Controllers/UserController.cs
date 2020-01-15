using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using core_api.Model;
using core_api.Database;
using Microsoft.EntityFrameworkCore;

namespace core_api.Controllers
{
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
        public IEnumerable<User> Get()
        {
            var rng = new Random();
            return _context.Users.Include("Posts").Where(item => item.username=="johni");
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            return new User() {
                id = id,
                email = "asdf@email.com",
                profile = "hello world",
                salt ="adsf",
                username = "asdf"
            };
        }

        [HttpPost]
        public User AddUser(User user)
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
            return user;
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

    }
}

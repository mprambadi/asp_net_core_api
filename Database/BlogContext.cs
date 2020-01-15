using System;
using Microsoft.EntityFrameworkCore;
using core_api.Model;

namespace core_api.Database
{

    public class BlogContext: DbContext 
    {
        public BlogContext(DbContextOptions<BlogContext> options): base(options){}

        public DbSet<User> Users {get;set;}
        public DbSet<Post> Posts {get;set;}
    
    }
}
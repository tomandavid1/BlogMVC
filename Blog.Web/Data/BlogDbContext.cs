using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    // inherit from entity framework
    public class BlogDbContext : DbContext
    {
        //constructor with options
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        // reference to domain models
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}

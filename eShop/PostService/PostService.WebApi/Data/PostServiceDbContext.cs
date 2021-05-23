using Microsoft.EntityFrameworkCore;
using PostService.WebApi.Entities;

namespace PostService.WebApi.Data
{
  public class PostServiceDbContext : DbContext
  {
    public PostServiceDbContext(DbContextOptions<PostServiceDbContext> options) : base(options) { }

    public DbSet<Post> Post { get; set; }
    public DbSet<User> User { get; set; }
  }
}

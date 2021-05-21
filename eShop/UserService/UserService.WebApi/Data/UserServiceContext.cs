namespace UserService.WebApi.Data
{
  using Microsoft.EntityFrameworkCore;
  using UserService.WebApi.Entity;

  public class UserServiceContext : DbContext
  {
    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
  }
}

namespace UserService.WebApi.Data
{
  using Microsoft.EntityFrameworkCore;
  using UserService.WebApi.Entities;

  public class UserServiceDbContext : DbContext
  {
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
  }
}

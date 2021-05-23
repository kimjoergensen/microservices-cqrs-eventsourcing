namespace UserService.WebApi.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using UserService.WebApi.Data;
  using UserService.WebApi.Entities;

  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly UserServiceDbContext _context;

    public UserController(UserServiceDbContext context) {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser() {
      return await _context.User.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id) {
      var user = await _context.User.FindAsync(id);

      if (user == null) return NotFound();
      return user;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutUser(User user) {
      _context.Entry(user).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user) {
      _context.User.Add(user);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
  }
}

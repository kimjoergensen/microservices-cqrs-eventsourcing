namespace UserService.WebApi.Controller
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using UserService.WebApi.Data;
  using UserService.WebApi.Entity;

  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly UserServiceContext _context;

    public UserController(UserServiceContext context) {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
      => await _context.User.ToListAsync();

    [HttpPut("{id}")]
    public async Task<ActionResult> PutUser(int id, User user) {
      _context.Entry(user).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user) {
      _context.User.Add(user);
      await _context.SaveChangesAsync();
      return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }
  }
}

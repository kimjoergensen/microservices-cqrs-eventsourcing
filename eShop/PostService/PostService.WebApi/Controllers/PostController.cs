namespace PostService.WebApi.Controllers
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using PostService.WebApi.Data;
  using PostService.WebApi.Entities;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  [Route("api/[controller]")]
  [ApiController]
  public class PostController : ControllerBase
  {
    private readonly PostServiceDbContext _context;

    public PostController(PostServiceDbContext context) {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPost() {
      return await _context.Post.Include(post => post.User).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id) {
      return await _context.Post.FindAsync(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPost(int id, Post post) {
      if (id != post.Id) return BadRequest();
      _context.Entry(post).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      } catch (DbUpdateConcurrencyException) {
        if (!await PostExists(id)) return NotFound();
        else throw;
      }

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Post>> PostPost(Post post) {
      _context.Post.Add(post);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePost(int id) {
      var post = await _context.Post.FindAsync(id);
      if (post == null) return NotFound();

      _context.Post.Remove(post);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private Task<bool> PostExists(int id) =>
      _context.Post.AnyAsync(post => post.Id == id);
  }
}

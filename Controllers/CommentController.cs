// Controllers/CommentController.cs
using ArtTrade.Data;
using ArtTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm yorumları getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        return await _context.Comments.ToListAsync();
    }

    // CREATE: Yeni yorum ekleme
    [HttpPost]
    public IActionResult CreateComment([FromBody] Comment newComment)
    {
        if (ModelState.IsValid)
        {
            _context.Comments.Add(newComment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id }, newComment);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir yorumu ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetCommentById(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return comment;
    }

    // UPDATE: Belirli bir yorumu güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment updatedComment)
    {
        if (id != updatedComment.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedComment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CommentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: Belirli bir yorumu silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CommentExists(int id)
    {
        return _context.Comments.Any(e => e.Id == id);
    }
}

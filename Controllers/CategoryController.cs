// Controllers/CategoryController.cs
using ArtTrade.Data;
using ArtTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm kategorileri getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    // CREATE: Yeni kategori ekleme
    [HttpPost]
    public IActionResult CreateCategory([FromBody] Category newCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir kategoriyi ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    // UPDATE: Belirli bir kategoriyi güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category updatedCategory)
    {
        if (id != updatedCategory.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
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

    // DELETE: Belirli bir kategoriyi silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}

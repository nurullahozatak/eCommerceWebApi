// Controllers/SellerController.cs
using ArtTrade.Data;
using ArtTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SellerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SellerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm satıcıları getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Seller>>> GetSellers()
    {
        return await _context.Sellers.ToListAsync();
    }

    // CREATE: Yeni satıcı ekleme
    [HttpPost]
    public IActionResult CreateSeller([FromBody] Seller newSeller)
    {
        if (ModelState.IsValid)
        {
            _context.Sellers.Add(newSeller);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSellerById), new { id = newSeller.Id }, newSeller);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir satıcıyı ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<Seller>> GetSellerById(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);

        if (seller == null)
        {
            return NotFound();
        }

        return seller;
    }

    // UPDATE: Belirli bir satıcıyı güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSeller(int id, [FromBody] Seller updatedSeller)
    {
        if (id != updatedSeller.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedSeller).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SellerExists(id))
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

    // DELETE: Belirli bir satıcıyı silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeller(int id)
    {
        var seller = await _context.Sellers.FindAsync(id);

        if (seller == null)
        {
            return NotFound();
        }

        _context.Sellers.Remove(seller);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SellerExists(int id)
    {
        return _context.Sellers.Any(e => e.Id == id);
    }
}

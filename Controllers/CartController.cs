// Controllers/CartController.cs
using ArtTrade.Data;
using ArtTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CartController(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    // READ: Tüm sepetleri getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
        return await _dbContext.Carts.ToListAsync();
    }

    // CREATE: Yeni sepet ekleme
    [HttpPost]
    public IActionResult CreateCart([FromBody] Cart newCart)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Carts.Add(newCart);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetCartById), new { id = newCart.Id }, newCart);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir sepeti ID ile getirme
    [HttpGet("{Id}")]
    public async Task<ActionResult<Cart>> GetCartById(int cartId)
    {
        var cart = await _dbContext.Carts.FindAsync(cartId);

        if (cart == null)
        {
            return NotFound();
        }

        return cart;
    }

    // UPDATE: Belirli bir sepeti güncelleme
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateCart(int cartId, [FromBody] Cart updatedCart)
    {
        if (cartId != updatedCart.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(updatedCart).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CartExists(cartId))
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

    // DELETE: Belirli bir sepeti silme
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteCart(int cartId)
    {
        var cart = await _dbContext.Carts.FindAsync(cartId);

        if (cart == null)
        {
            return NotFound();
        }

        _dbContext.Carts.Remove(cart);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool CartExists(int cartId)
    {
        return _dbContext.Carts.Any(e => e.Id == cartId);
    }
}

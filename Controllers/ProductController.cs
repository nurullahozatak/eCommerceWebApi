// Controllers/ProductController.cs
using ArtTrade.Models;
using ArtTrade.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm ürünleri getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // CREATE: Yeni ürün ekleme
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product newProduct)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir ürünü ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // UPDATE: Belirli bir ürünü güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        if (id != updatedProduct.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedProduct).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
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

    // DELETE: Belirli bir ürünü silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}

// Controllers/OrderController.cs
using ArtTrade.Models;
using ArtTrade.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm siparişleri getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    // CREATE: Yeni sipariş ekleme
    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order newOrder)
    {
        if (ModelState.IsValid)
        {
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir siparişi ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    // UPDATE: Belirli bir siparişi güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order updatedOrder)
    {
        if (id != updatedOrder.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
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

    // DELETE: Belirli bir siparişi silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }
}
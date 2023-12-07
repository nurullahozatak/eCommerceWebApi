// Controllers/CustomerController.cs
using ArtTrade.Data;
using ArtTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm müşterileri getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    // CREATE: Yeni müşteri ekleme
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] Customer newCustomer)
    {
        if (ModelState.IsValid)
        {
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir müşteriyi ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }

    // UPDATE: Belirli bir müşteriyi güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
    {
        if (id != updatedCustomer.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedCustomer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
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

    // DELETE: Belirli bir müşteriyi silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CustomerExists(int id)
    {
        return _context.Customers.Any(e => e.Id == id);
    }
}

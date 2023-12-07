using ArtTrade.Data;
using ArtTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure this line is added

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CouponController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm kuponları getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
    {
        return await _context.Coupons.ToListAsync();
    }

    // CREATE: Yeni kupon ekleme
    [HttpPost]
    public IActionResult CreateCoupon([FromBody] Coupon newCoupon)
    {
        if (ModelState.IsValid)
        {
            _context.Coupons.Add(newCoupon);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCouponById), new { id = newCoupon.Id }, newCoupon);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir kuponu ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<Coupon>> GetCouponById(int id)
    {
        var coupon = await _context.Coupons.FindAsync(id);

        if (coupon == null)
        {
            return NotFound();
        }

        return coupon;
    }

    // UPDATE: Belirli bir kuponu güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCoupon(int id, [FromBody] Coupon updatedCoupon)
    {
        if (id != updatedCoupon.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedCoupon).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CouponExists(id))
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

    // DELETE: Belirli bir kuponu silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupon(int id)
    {
        var coupon = await _context.Coupons.FindAsync(id);

        if (coupon == null)
        {
            return NotFound();
        }

        _context.Coupons.Remove(coupon);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CouponExists(int id)
    {
        return _context.Coupons.Any(e => e.Id == id);
    }
}

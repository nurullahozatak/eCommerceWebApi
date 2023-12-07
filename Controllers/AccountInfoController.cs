using ArtTrade.Data;
using ArtTrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AccountInfoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AccountInfoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: Tüm account bilgilerini getirme
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountInfo>>> GetAccountInfos()
    {
        return await _context.AccountInfos.ToListAsync();
    }

    // CREATE: Yeni account bilgisi ekleme
    [HttpPost]
    public IActionResult CreateAccountInfo([FromBody] AccountInfo newAccountInfo)
    {
        if (ModelState.IsValid)
        {
            _context.AccountInfos.Add(newAccountInfo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAccountInfoById), new { id = newAccountInfo.Id }, newAccountInfo);
        }
        return BadRequest(ModelState);
    }

    // READ: Belirli bir account bilgisini ID ile getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountInfo>> GetAccountInfoById(int id)
    {
        var accountInfo = await _context.AccountInfos.FindAsync(id);

        if (accountInfo == null)
        {
            return NotFound();
        }

        return accountInfo;
    }

    // UPDATE: Belirli bir account bilgisini güncelleme
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccountInfo(int id, [FromBody] AccountInfo updatedAccountInfo)
    {
        if (id != updatedAccountInfo.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedAccountInfo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AccountInfoExists(id))
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

    // DELETE: Belirli bir account bilgisini silme
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccountInfo(int id)
    {
        var accountInfo = await _context.AccountInfos.FindAsync(id);

        if (accountInfo == null)
        {
            return NotFound();
        }

        _context.AccountInfos.Remove(accountInfo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AccountInfoExists(int id)
    {
        return _context.AccountInfos.Any(e => e.Id == id);
    }
}

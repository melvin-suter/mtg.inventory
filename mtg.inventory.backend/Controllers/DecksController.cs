using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DecksController : ControllerBase
{
    private readonly DefaultDBContext _context;

    public DecksController(DefaultDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetDecks()
    {
        var decks = await _context.Deck.ToListAsync();
        if (decks is null || decks.Count == 0)
        {
            return NoContent();
        }

        return Ok(decks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetDeck(int id)
    {
        var deck = await _context.Deck.FindAsync(id);

        if (deck is null)
        {
            return NotFound();
        }

        return Ok(deck);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeck(int id, Deck deck)
    {
        if (id != deck.Id)
        {
            return BadRequest();
        }

        if (!await _context.Deck.AnyAsync(x => x.Id == id))
        {
            return NotFound();
        }

        _context.Deck.Update(deck);

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Deck>> PostDeck(Deck deck)
    {

        _context.Deck.Add(deck);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDeck", new 
        { 
            deck.Id 
        }, deck);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeck(int id)
    {

        var deck = await _context.Deck.FindAsync(id);
        if (deck == null)
        {
            return NotFound();
        }

        _context.Deck.Remove(deck);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

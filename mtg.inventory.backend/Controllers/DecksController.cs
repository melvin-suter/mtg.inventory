using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly DefaultDBContext _context;

        public DecksController(DefaultDBContext context)
        {
            _context = context;
        }

        // GET: api/Decks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deck>>> GetDeck()
        {
          if (_context.Deck == null)
          {
              return NotFound();
          }
            return await _context.Deck.ToListAsync();
        }

        // GET: api/Decks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deck>> GetDeck(int id)
        {
          if (_context.Deck == null)
          {
              return NotFound();
          }
            var deck = await _context.Deck.FindAsync(id);

            if (deck == null)
            {
                return NotFound();
            }

            return deck;
        }

        // PUT: api/Decks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeck(int id, Deck deck)
        {
            if (id != deck.id)
            {
                return BadRequest();
            }

            _context.Entry(deck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckExists(id))
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

        // POST: api/Decks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Deck>> PostDeck(Deck deck)
        {
          if (_context.Deck == null)
          {
              return Problem("Entity set 'DefaultDBContext.Deck'  is null.");
          }
            _context.Deck.Add(deck);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeck", new { id = deck.id }, deck);
        }

        // DELETE: api/Decks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeck(int id)
        {
            if (_context.Deck == null)
            {
                return NotFound();
            }
            var deck = await _context.Deck.FindAsync(id);
            if (deck == null)
            {
                return NotFound();
            }

            _context.Deck.Remove(deck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeckExists(int id)
        {
            return (_context.Deck?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

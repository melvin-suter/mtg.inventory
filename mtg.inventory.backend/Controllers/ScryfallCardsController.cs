using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace mtg_inventory_backend.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class ScryfallCardsController : ControllerBase
    {
        private readonly DefaultDBContext _context;

        public ScryfallCardsController(DefaultDBContext context)
        {
            _context = context;
        }


        [HttpGet("{id}/update")]
        public async Task<IActionResult> updateScryfallData(string id){

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.scryfall.com/cards/" + id);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadFromJsonAsync<IEnumerable<dynamic>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}", d);
                }
            }

            client.Dispose();

            return NoContent();
        }

        // GET: api/ScryfallCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScryfallCard>>> GetScryfallCard()
        {
          if (_context.ScryfallCard == null)
          {
              return NotFound();
          }
            return await _context.ScryfallCard.ToListAsync();
        }

        // GET: api/ScryfallCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScryfallCard>> GetScryfallCard(string id)
        {
          if (_context.ScryfallCard == null)
          {
              return NotFound();
          }
            var scryfallCard = await _context.ScryfallCard.FindAsync(id);

            if (scryfallCard == null)
            {
                return NotFound();
            }

            return scryfallCard;
        }

        // PUT: api/ScryfallCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScryfallCard(string id, ScryfallCard scryfallCard)
        {
            if (id != scryfallCard.id)
            {
                return BadRequest();
            }

            _context.Entry(scryfallCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScryfallCardExists(id))
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

        // POST: api/ScryfallCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScryfallCard>> PostScryfallCard(ScryfallCard scryfallCard)
        {
          if (_context.ScryfallCard == null)
          {
              return Problem("Entity set 'DefaultDBContext.ScryfallCard'  is null.");
          }
            _context.ScryfallCard.Add(scryfallCard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScryfallCardExists(scryfallCard.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScryfallCard", new { id = scryfallCard.id }, scryfallCard);
        }

        // DELETE: api/ScryfallCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScryfallCard(string id)
        {
            if (_context.ScryfallCard == null)
            {
                return NotFound();
            }
            var scryfallCard = await _context.ScryfallCard.FindAsync(id);
            if (scryfallCard == null)
            {
                return NotFound();
            }

            _context.ScryfallCard.Remove(scryfallCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScryfallCardExists(string id)
        {
            return (_context.ScryfallCard?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

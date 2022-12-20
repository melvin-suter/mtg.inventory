using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers
{
    [Route("api/collections")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly DefaultDBContext _context;

        public CollectionsController(DefaultDBContext context)
        {
            _context = context;
        }

        #region  Collections

        // GET: api/Collections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollection()
        {
            if (_context.Collection == null)
            {
                return NotFound();
            }
            return await _context.Collection.ToListAsync();
        }

        // GET: api/Collections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collection>> GetCollection(int id)
        {
            if (_context.Collection == null)
            {
                return NotFound();
            }
            var collection = await _context.Collection.FindAsync(id);

            if (collection == null)
            {
                return NotFound();
            }

            return collection;
        }

        // PUT: api/Collections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {
            if (id != collection.id)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(id))
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

        // POST: api/Collections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {
          if (_context.Collection == null)
          {
              return Problem("Entity set 'DefaultDBContext.Collection'  is null.");
          }
            _context.Collection.Add(collection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollection", new { id = collection.id }, collection);
        }

        // DELETE: api/Collections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            if (_context.Collection == null)
            {
                return NotFound();
            }
            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            _context.Collection.Remove(collection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Folders

        [HttpGet("{colId}/folders")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolders(int colId){
            if (_context.Collection == null)
            {
                return NotFound();
            }
            var collection = await _context.Collection.FindAsync(colId);

            if (collection == null)
            {
                return NotFound();
            }

            return _context.Folder.AsEnumerable<Folder>().Where((Folder fol, int bol) => fol.collectionId == colId).ToList();
        }

        // GET: api/Folders/5
        [HttpGet("{colId}/folders/{folId}")]
        public async Task<ActionResult<Folder>> GetFolder(int folId)
        {
          if (_context.Folder == null)
          {
              return NotFound();
          }
            var folder = await _context.Folder.FindAsync(folId);

            if (folder == null)
            {
                return NotFound();
            }

            return folder;
        }

        [HttpPost("{colId}/folders")]
        public async Task<ActionResult<Folder>> PostFolder(int colId, Folder folder)
        {
            if (_context.Collection == null)
            {
                return NotFound();
            }
            var collection = await _context.Collection.FindAsync(colId);
            if (collection == null)
            {
                return NotFound();
            }


            if (_context.Folder == null)
            {
                return Problem("Entity set 'DefaultDBContext.Folder'  is null.");
            }

            if(collection.folders == null) { collection.folders = new List<Folder>();}
            collection.folders.Add(folder);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolder", new { colId = colId, folId = folder.id }, folder);
        }

        // DELETE: api/Folders/5
        [HttpDelete("{colId}/folders/{folId}")]
        public async Task<IActionResult> DeleteFolder(int colId, int folId)
        {
            if (_context.Folder == null)
            {
                return NotFound();
            }
            var folder = await _context.Folder.FindAsync(folId);
            if (folder == null)
            {
                return NotFound();
            }

            _context.Folder.Remove(folder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Folders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{colId}/folders/{folId}")]
        public async Task<IActionResult> PutFolder(int folId, Folder folder)
        {
            if (folId != folder.id)
            {
                return BadRequest();
            }

            _context.Entry(folder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FolderExists(folId))
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

        #endregion

        #region Cards 

        // GET: api/Cards
        [HttpGet("{colId}/folders/{folId}/cards")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCard(int colId, int folId)
        {
          if (_context.Card == null)
          {
              return NotFound();
          }
            var folder = await _context.Folder.FindAsync(folId);

            if (folder == null)
            {
                return NotFound();
            }

            return folder.cards;
        }

        // GET: api/Cards/5
        [HttpGet("{colId}/folders/{folId}/cards/{cardId}")]
        public async Task<ActionResult<Card>> GetCard(int cardId)
        {
          if (_context.Card == null)
          {
              return NotFound();
          }
            var card = await _context.Card.FindAsync(cardId);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{colId}/folders/{folId}/cards/{cardId}")]
        public async Task<IActionResult> PutCard(int cardId, Card card)
        {
            if (cardId != card.id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(cardId))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{colId}/folders/{folId}/cards/")]
        public async Task<ActionResult<Card>> PostCard(int colId, int folId, Card card)
        {
            if (_context.Card == null)
            {
                return Problem("Entity set 'DefaultDBContext.Card'  is null.");
            }
             if (_context.Folder == null)
            {
                return NotFound();
            }
                var folder = await _context.Folder.FindAsync(folId);

            if (folder == null)
            {
                return NotFound();
            }

            if(folder.cards == null) { folder.cards = new List<Card>();}
            folder.cards.Add(card);
            _context.Entry(card).State = EntityState.Added;
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { colId = colId, folId = folId, id = card.id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{colId}/folders/{folId}/cards/{cardId}")]
        public async Task<IActionResult> DeleteCard(int cardId)
        {
            if (_context.Card == null)
            {
                return NotFound();
            }
            var card = await _context.Card.FindAsync(cardId);
            if (card == null)
            {
                return NotFound();
            }

            _context.Card.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Exists
        private bool CardExists(int id)
        {
            return (_context.Card?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private bool CollectionExists(int id)
        {
            return (_context.Collection?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private bool FolderExists(int id)
        {
            return (_context.Folder?.Any(e => e.id == id)).GetValueOrDefault();
        }
        #endregion
    }
}

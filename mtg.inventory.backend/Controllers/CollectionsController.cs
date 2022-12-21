using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers;

[Route("api/collections")]
[ApiController]
public class CollectionsController : ControllerBase
{
    private readonly DefaultDBContext _context;

    public CollectionsController(DefaultDBContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetCollection()
    {
        return Ok(await _context.Collection.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCollection(int id)
    {
        var collection = await _context.Collection.FindAsync(id);

        if (collection is null)
        {
            return NotFound();
        }

        return Ok(collection);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutCollection(int id, Collection collection)
    {
        if (id != collection.Id)
        {
            return BadRequest();
        }

        if ((await _context.Collection.FindAsync(id)) is null)
        {
            return NotFound();
        }

        _context.Collection.Update(collection);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Collection>> PostCollection(Collection collection)
    {
        _context.Collection.Add(collection);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCollection", new 
        { 
            collection.Id 
        }, collection);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCollection(int id)
    {
        var collection = await _context.Collection.FindAsync(id);
        if (collection is null)
        {
            return NotFound();
        }

        _context.Collection.Remove(collection);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

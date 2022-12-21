using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers;

[Route("api/collections/{collectionId}/folders")]
[ApiController]
public class CollectionFoldersController : ControllerBase
{
    private readonly DefaultDBContext _context;

    public CollectionFoldersController(DefaultDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetFolders(int collectionId)
    {
        var collection = await _context.Collection.FindAsync(collectionId);
        if (collection is null)
        {
            return NotFound();
        }

        if (collection.Folders is null || collection.Folders.Count == 0)
        {
            return NoContent();
        }

        return Ok(collection.Folders);

    }

    [HttpGet("{folderId}")]
    public async Task<ActionResult> GetFolder(int collectionId, int folderId)
    {
        var collection = await _context.Collection.FindAsync(collectionId);
        if (collection is null)
        {
            return NotFound();
        }

        var folder = collection.Folders?.FirstOrDefault(x => x.Id.Equals(folderId));
        if (folder is null)
        {
            return NotFound();
        }

        return Ok(folder);
    }

    [HttpPost]
    public async Task<ActionResult> PostFolder(int collectionId, Folder folder)
    {
        var collection = await _context.Collection.FindAsync(collectionId);
        if (collection is null)
        {
            return NotFound();
        }

        (collection.Folders ??= new List<Folder>()).Add(folder);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFolder", new
        {
            CollectionId = collectionId,
            FolderId = folder.Id
        });
    }

    [HttpDelete("{folderId}")]
    public async Task<ActionResult> DeleteFolder(int collectionId, int folderId)
    {
        var collection = await _context.Collection.FindAsync(collectionId);
        if (collection is null)
        {
            return NotFound();
        }

        var folder = collection.Folders?.FirstOrDefault(x => x.Id.Equals(folderId));
        if (folder is null)
        {
            return NotFound();
        }

        collection.Folders!.Remove(folder);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{folderId}")]
    public async Task<ActionResult> PutFolder(int collectionId, int folderId, Folder folder)
    {
        if (!folderId.Equals(folder.Id) || !collectionId.Equals(folder.CollectionId))
        {
            return BadRequest();
        }

        if (await _context.Collection.FindAsync(collectionId) is null)
        {
            return NotFound();
        }

        if (await _context.Folder.FindAsync(folderId) is null)
        {
            return NotFound();
        }

        _context.Update(folder);

        await _context.SaveChangesAsync();

        return NoContent();
    }

}

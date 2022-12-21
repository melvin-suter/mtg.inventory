using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers;

[Route("api/collections/{collectionId}/folders/{folderId}/cards")]
[ApiController]
public class CollectionFolderCardsController : ControllerBase
{
    private readonly DefaultDBContext _context;
    private readonly IScryfallService _scryfallService;

    public CollectionFolderCardsController(DefaultDBContext context, IScryfallService scryfallService)
    {
        _context = context;
        _scryfallService = scryfallService;
    }

    [HttpGet]
    public async Task<ActionResult> GetCards(int folderId)
    {
        var folder = await _context.Folder.FindAsync(folderId);

        if (folder is null)
        {
            return NotFound();
        }

        var cards = folder.Cards;
        if (cards is null || cards.Count == 0)
        {
            return NoContent();
        }

        return Ok(cards);
    }

    [HttpGet("{cardId}")]
    public async Task<ActionResult> GetCard(int folderId, int cardId)
    {
        var folder = await _context.Folder.FindAsync(folderId);

        if (folder is null)
        {
            return NotFound();
        }

        var cards = folder.Cards;
        if (cards is null || cards.Count == 0)
        {
            return NotFound();
        }

        var card = cards.FirstOrDefault(x => x.Id == cardId);
        if (card is null)
        {
            return NotFound();
        }

        return Ok(card);
    }

    [HttpPut("{cardId}")]
    public async Task<ActionResult> PutCard(int folderId, int cardId, FolderCard card)
    {
        if (cardId != card.Id || card.FolderId != folderId)
        {
            return BadRequest();
        }

        var folder = await _context.Folder.FindAsync(folderId);

        if (folder is null)
        {
            return NotFound();
        }

        var cards = folder.Cards;
        if (cards is null || cards.Count == 0)
        {
            return NotFound();
        }

        if (!folder.Cards.Any(x => x.Id == cardId))
        {
            return NotFound();
        }

        _context.FolderCard.Update(card);

        //Check if card needs to be added
        if (!await _context.CardMetadata.AnyAsync(x => x.Id.Equals(card.CardMetadataId)))
        {
            var metadata = await _scryfallService.GetCard(card.CardMetadataId);
            if (metadata is null)
            {
                //We ignore this for now...
            }
            else
            {
                _context.CardMetadata.Add(metadata);
            }
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{cardId}")]
    public async Task<ActionResult> PostCard(int folderId, FolderCard card)
    {
        if (folderId != card.FolderId)
        {
            return BadRequest();
        }

        var folder = await _context.Folder.FindAsync(folderId);

        if (folder is null)
        {
            return NotFound();
        }

        (folder.Cards ??= new List<FolderCard>()).Add(card);

        //Check if card needs to be added
        if (!await _context.CardMetadata.AnyAsync(x => x.Id.Equals(card.CardMetadataId)))
        {
            var metadata = await _scryfallService.GetCard(card.CardMetadataId);
            if (metadata is null)
            {
                //We ignore this for now...
            }
            else
            {
                _context.CardMetadata.Add(metadata);
            }
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCard", new
        {
            card.Id
        }, card);
    }

    [HttpDelete("{cardId}")]
    public async Task<ActionResult> DeleteCard(int folderId, int cardId)
    {
        var folder = await _context.Folder.FindAsync(folderId);

        if (folder is null)
        {
            return NotFound();
        }

        var card = folder.Cards?.FirstOrDefault(x => x.Id == cardId);
        if (card is null)
        {
            return NotFound();
        }

        folder.Cards!.Remove(card);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

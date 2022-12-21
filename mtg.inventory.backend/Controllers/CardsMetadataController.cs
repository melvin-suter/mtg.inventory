using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers;

[Route("api/cards")]
[ApiController]
public class CardsMetadataController : ControllerBase
{
    private readonly DefaultDBContext _context;
    private readonly IScryfallService _scryfallService;

    public CardsMetadataController(DefaultDBContext context, IScryfallService scryfallService)
    {
        _context = context;
        _scryfallService = scryfallService;
    }

    [HttpGet("{cardId}")]
    public async Task<ActionResult> GetCardMetadata(string id)
    {
        var metadata = await _context.CardMetadata.FindAsync(id);
        if (metadata is null)
        {
            //As per request of our glorius repo overlord, return an empty object :)
            return Ok(new CardMetadata());
        }

        return Ok(metadata);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateCardMetadata()
    {
        var cards = await _context.CardMetadata.ToListAsync();
        var cardUpdateCount = 0;
        foreach (var card in cards)
        {
            var scryfallCard = await _scryfallService.GetCard(card.Id);
            if (scryfallCard is null)
            {
                continue;
            }

            _context.CardMetadata.Update(scryfallCard);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            cardUpdateCount
        });
    }

    [HttpPatch("{cardId}")]
    public async Task<ActionResult> UpdateCardMetadata(string cardId)
    {
        var card = await _context.CardMetadata.FindAsync(cardId);
        if (card is null)
        {
            return NotFound();
        }

        var scryfallCard = await _scryfallService.GetCard(cardId);
        if (scryfallCard is null)
        {
            return NoContent();
        }

        _context.CardMetadata.Update(scryfallCard);
        await _context.SaveChangesAsync();

        return Ok(scryfallCard);
    }
}

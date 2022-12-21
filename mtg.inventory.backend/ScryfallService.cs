using mtg_inventory_backend.Models;

namespace mtg_inventory_backend;

public class ScryfallService : IScryfallService, IDisposable
{
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("https://api.scryfall.com")
    };

    public async Task<CardMetadata?> GetCard(string id)
    {
        var response = await _client.GetAsync($"/cards/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null; 
        }

        var card = await response.Content.ReadFromJsonAsync<dynamic>();
        if (card is null)
        {
            return null;
        }

        return new CardMetadata
        {
            Id = card.id,
            Name = card.name,
            Lang = card.lang,
            Layout = card.layout,
            ImageUrl_Small = card.image_uris.small,
            ImageUrl_Big = card.image_uris.large,
            Type = card.type_line,
            Colors = new List<string>(card.colors),
            Keywords = new List<string>(card.keywords),
            Color_identity = new List<string>(card.color_identity),
            Mana_cost = card.mana_cost,
            Oracle_text = card.oracle_text,
            Power = card.power,
            Toughness = card.toughness,
            Mana_cost_total = card.cmc,
        };
    }

    public void Dispose() 
    { 
        GC.SuppressFinalize(this);
        _client?.Dispose();
    }
}

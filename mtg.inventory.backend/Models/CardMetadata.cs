namespace mtg_inventory_backend.Models;

public class CardMetadata
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Lang { get; set; }
    public string? Layout { get; set; }
    public string ImageUrl_Small { get; set; } = null!;
    public string ImageUrl_Big { get; set; } = null!;
    public string? Type { get; set; }
    public List<string>? Colors { get; set; }
    public List<string>? Keywords { get; set; }
    public List<string>? Color_identity { get; set; }
    public string? Mana_cost { get; set; }
    public string? Oracle_text { get; set; }
    public int? Power { get; set; }
    public int? Toughness { get; set; }
    public int? Mana_cost_total { get; set; } // cmc

}
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend;

public interface IScryfallService
{
    public Task<CardMetadata?> GetCard(string id);
}

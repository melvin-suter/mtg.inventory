using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace mtg_inventory_backend.Models;

public class ScryfallCard
{
    public string id { get; set; }
    
    [Required]
    public string name { get; set; }
    
    public string? lang {get;set;}
    public string? layout {get;set;}
    public string imageUrl_Small {get;set;}
    public string imageUrl_Big {get;set;}
    public string? type {get;set;}
    public List<string>? colors {get;set;}
    public List<string>? keywords {get;set;}
    public string? color_identity {get;set;}
    public string? mana_cost {get;set;}
    public string? oracle_text {get;set;}
    public int? power {get;set;}
    public int? toughness {get;set; }
    public int? mana_cost_total {get;set;} // cmc

}
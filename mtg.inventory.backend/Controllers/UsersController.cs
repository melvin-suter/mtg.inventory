using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Controllers;

[Route("api/auth")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DefaultDBContext _context;

    public UsersController(DefaultDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _context.User.ToListAsync();
        if (users is null || users.Count == 0)
        {
            return NoContent();
        }

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = await _context.User.FindAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        if (!await _context.User.AnyAsync(x => x.Id.Equals(id)))
        {
            return NotFound();
        }

        _context.User.Update(user);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(User user)
    {

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new 
        { 
            user.Id 
        }, user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

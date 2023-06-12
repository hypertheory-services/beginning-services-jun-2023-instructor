using LocationsApi.Models;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace LocationsApi.Controllers;

public class LocationsController : ControllerBase
{
    private readonly IDocumentSession _context;

    public LocationsController(IDocumentSession context)
    {
        _context = context;
    }

    [HttpGet("/locations")]
    public async Task<ActionResult>  GetLocations()
    {
        var data = await _context.Query<LocationItemResponse>().ToListAsync();
        var response = new LocationsResponse { _embedded = data };
        return Ok(response);
    }

    [HttpPost("/locations")]
    public async Task<ActionResult> AddLocation([FromBody] LocationCreate request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400
        }

        var locationToAdd = new LocationItemResponse
        {
            Name = request.Name,
            Description = request.Description,
            Id = Guid.NewGuid().ToString(),
            AddedBy = "bob",
            AddedOn = DateTime.Now,
        };
        _context.Store<LocationItemResponse>(locationToAdd);
        // add all the things.
        await _context.SaveChangesAsync();

        return Ok(locationToAdd); 
    }
}

using Marten;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Entities;
using Shows.Api.Models;

namespace Shows.Api;

public class Controller(IDocumentSession session) : ControllerBase
{

    [HttpPost("/api/shows")]
    // public async Task<ActionResult> AddVendorAsync([FromBody] string show)
    public async Task<ActionResult> AddVendorAsync([FromBody] ShowCreateModel showModel)
    {
        //validate stuff
        var entity = new ShowEntity
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTimeOffset.UtcNow,
            Title = showModel.Title,
            Description = showModel.Description,
            StreamingService = showModel.StreamingService
        };
        session.Store(entity);  
        await session.SaveChangesAsync();

        var response = new ShowDetailsModel
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            Title = entity.Title,
            Description = entity.Description,
            StreamingService = entity.StreamingService
        };
        return StatusCode(200, response); // "Created"


    }

    [HttpGet("/api/shows/{id:guid}")]
    public async Task<ActionResult> GetVendorByIdAsync(Guid id)
    {
        var savedShow = await session.Query<ShowEntity>()
            .Where(v => v.Id == id)
            .SingleOrDefaultAsync();
        if (savedShow == null)
        {
            return NotFound();
        }
        else
        {
            var response = new ShowDetailsModel
            {
                Id = savedShow.Id,
                CreatedAt = savedShow.CreatedAt,
                Title = savedShow.Title,
                Description = savedShow.Description,
                StreamingService = savedShow.StreamingService      
            };
            return Ok(response);
        }
    }

}

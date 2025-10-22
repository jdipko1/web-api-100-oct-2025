using Marten;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Utilities;
using Shows.Api.Entities;
using Shows.Api.Models;

namespace Shows.Api;

public class Controller(IDocumentSession session) : ControllerBase
{

    [HttpPost("/api/shows")]
    // public async Task<ActionResult> AddVendorAsync([FromBody] string show)
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] ShowCreateModel showModel 
        /*, [FromServices] ShowsCreateModelValidator validator*/ )
    {
        //validate stuff
        /*
        var validations = await validator.ValidateAsync(showModel);

        if (!validations.IsValid)
        {
            return BadRequest();
        }
        */

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

    [HttpGet("api/shows")]
    public async Task<ActionResult> GetAllShows()
    {
        var shows = await session.Query<ShowEntity>()
                    .OrderBy(v => v.CreatedAt).ToListAsync<ShowEntity>();
        var response = new CollectionResponseModel<ShowSummaryItem>
        {
            Data = [.. shows.Select(v => new ShowSummaryItem
            {
                Id = v.Id,
                Title = v.Title,
                Description  = v.Description,
                StreamingService = v.StreamingService,
                CreatedAt = v.CreatedAt
            })]
        };

        return Ok(response);
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

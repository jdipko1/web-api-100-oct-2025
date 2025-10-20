
namespace SoftwareCenter.Api.Controllers;
public class VendorsController : Controller
{
    [HttpGet("/vendors")]
   public async Task<ActionResult> GetAllVendorsAsync()
    {
        return Ok();
    }


[HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorASync([FromBody] VendorCreateModel model)
    {
        var response = new VendorDetailsModel()
        {
            Id = new Guid(),
            Name = model.Name,
            PointOfContact = model.PointOfContact
        };
        return StatusCode(201, model); //created
    }
}

public record VendorPointOfContact
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;


}

public record VendorDetailsModel
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();


}

public record VendorCreateModel
{
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
}

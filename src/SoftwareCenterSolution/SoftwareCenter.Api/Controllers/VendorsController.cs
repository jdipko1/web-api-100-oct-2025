
namespace SoftwareCenter.Api.Controllers;
public class VendorsController : Controller
{
    [HttpGet("/vendors")]
   public async Task<ActionResult> GetAllVendorsAsync()
    {
        return Ok();
    }
}

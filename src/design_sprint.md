Copy this into a file in your repo called "design-sprint.md"
copy the following into that file and include your notes, and suggestions:
Removing a Catalog Item
If we were to add the ability to remove a catalog item from a vendor, what would that look like as an HTTP request?
'''http
DELETE /vendors/some_vendor_id_here/catalog-items/some_catalog_id_here
In Extensions.cs, can try to map a DELETE route under MapCatalogItems
group.MapDelete("/{vendorId:guid}/catalog-items/{id:guid}", DeleteACatalogItem.Handle);
In CatalogItems/Endpoints, add a class DeleteACatalogItem.cs
//This should return a 204 if the delete was OK, else return a 404 and a reason
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using SoftwareCenter.Api.CatalogItems.Entities;
using SoftwareCenter.Api.CatalogItems.Models;
using SoftwareCenter.Api.Vendors.Entities;
namespace SoftwareCenter.Api.CatalogItems.Endpoints;
public static class DeleteACatalogItem
{
public static async Task<Results<NoContent, NotFound<string>>> Handle(
Guid vendorID,
Guid catalogItemId,
IDocumentSession session
)
{
var doesVendorExist = await session.Query<VendorEntity>().AnyAsync(v => v.Id == vendorID);
if (doesVendorExist)
{
var doesCatalogItemExist = await session.Query<CatalogItem>().AnyAsync(v => v.Id == catalogItemId);
if (doesCatalogItemExist)
{
//probably need to validate the DB delete was successful
//maybe a try catch or something
//return some error response code if this didn't work
session.DeleteWhere<CatalogItem>(x => x.Id.Equals(catalogItemId));
await session.SaveChangesAsync();
            }
            else
            {
                return TypedResults.NotFound("No Vendor With That Id");
            }
            return TypedResults.NoContent();
        }
        else
        {
            return TypedResults.NotFound("No Vendor With That Id");
        }
    }
}

We only want Software Center Managers, or the Software Center team member that added it to be able to remove an item.
To add this requirement for manager to the DELETE route, first would need to add a policy for software center manager.
builder.Services.AddAuthorizationBuilder()
.AddPolicy("software-center-manager", pol =>
{
// you can do whatever here. look them up in your database, whatever.
pol.RequireRole("SoftwareCenter");
pol.RequireRole("Manager");
});
Then after that use a
.RequireAuthorization("software-center-manager") on the MapDelete above.  This would ensure only managers are deleting the catalog item.
For ensuring the "software member that added it" requirement, we would need to also add Authorization to the post, but would need to parse and store the name embedded in the inbound token, and store that in the database when the catalog item is added.  Within the Authorization delegate on the post, first Authenticate that it is a  user (via claims in the token data), and then parse the UserName from the user.Identity.Name.
Bottom line - need .RequireAuthorization PLUS parse Name from User.Identity, and store it in the database.
The name of each catalog item for a vendor must be unique.
Would this change our POST for catalog items?
Yes, it would because the POST would have to determine whether the POSTED catalog item for that particular vendor already exists in the database.  If it does, then a new catalog item can be added and a 200 (or 201) can be returned with, optionally, a JSON version of the added catalog item.
What would you return if it is not unique?
If the catalog item under a specific vendor already exists, a 400 (BadRequest) can be returned with an error message such as "Vendor {x} catalog item {y} already exists."
note: Names of catalog items only have to be unique per vendor. Different vendors can have items with the same names.
You would have to ensure a catalog item in the database and all of the mappers are able to access both the vendorID and the catalogItemID.  You would also have to ensure only
authorized users would have access to this.
Sketch out how you would implement this in your API here, and/or code it up.
static class AddingACatalogItem
{
public static async Task<Ok<CatalogItem>, BadRequest> Handle(
CatalogItemCreateModel request,
IDocumentSession session,
Guid vendorId,
Guid catalogItemId
)
{
var doesExist = await session.Query<CatalogItem>().AnyAsync(v => v.Id == catalogItemId && v.vendorId == vendorId);
if (doesExist)
{
var response = await session.Query<CatalogItem>().
var entity = new CatalogItem
{
Id = catalogItemId
VendorId = vendorId,
Name = request.Name, //This would be the user name adding the catalog item
Description = request.Description,
}; // Todo: Mapper would be nice, right?
            session.Store(entity);
            await session.SaveChangesAsync();
            return TypedResults.Ok(entity); // Make a response model for this.
        else // couldn't store this
            return TypedResults.BadRequest();
    }
}

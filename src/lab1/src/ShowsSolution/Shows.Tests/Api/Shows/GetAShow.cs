using Alba;
using Marten.Linq.SoftDeletes;
using Shows.Api.Models;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class GetAShow(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task GetShow()
    {
        var showToAdd = new ShowCreateModel
        {
           Title = "JeffGonzalez",
           Description = "Coding To Technotronic",
           StreamingService = "Yeppers"
        };

        var postResponse = await _host.Scenario(api =>
        {
            api.Post.Json(showToAdd).ToUrl("/api/shows");
            api.StatusCodeShouldBe(200);
        });

        var postEntityReturned = postResponse.ReadAsJson<ShowDetailsModel>();

        Assert.NotNull(postEntityReturned);

        Assert.True(postEntityReturned.Id != Guid.Empty);
        Assert.Equal(postEntityReturned.Title, showToAdd.Title);
        Assert.Equal(postEntityReturned.StreamingService, showToAdd.StreamingService);
        Assert.Equal(postEntityReturned.Description, showToAdd.Description);


        var getResponse = await _host.Scenario(api =>
        {
            api.Get.Url($"/api/shows/{postEntityReturned.Id}");
            api.StatusCodeShouldBeOk();

        });

        var getEntityReturned = getResponse.ReadAsJson<ShowDetailsModel>();
        Assert.NotNull(getEntityReturned);
        Assert.Equal(postEntityReturned, getEntityReturned);
    }

}
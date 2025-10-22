using Alba;
using Marten.Linq.SoftDeletes;
using Shows.Api.Models;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class GetAll(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task GetAllShows()
    {


        var getResponse =
                 await _host.Scenario(api =>
                {
                    api.Get.Url($"/api/shows");
                    api.StatusCodeShouldBeOk();

                });
        var showsList = getResponse.ReadAsJson<CollectionResponseModel<ShowSummaryItem>>(); 

        for (int i = 0; i < showsList.Data.Count-1; i++) {
        
            Assert.True( showsList.Data[i].CreatedAt <= 
                showsList.Data[i + 1].CreatedAt, 
                $"Item at index {i} is not less than or equal to item at index {i + 1}");
        }
    }

}
using Alba;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class AddingAShow(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task AddShow()
    {
        var response = await _host.Scenario(_ =>
        {
            _.Post.Json(new
            {
                Name = "Test Show",
                Description = "This is a test show",
                StreamingService = "HBO Max"
            }).ToUrl("/api/shows");
            _.StatusCodeShouldBeOk();
        });

        

    }
    [Fact]
    public async Task AddABadShow()
    {
        var response = await _host.Scenario(_ =>
        {
            _.Post.Json(new
            {
                Name = "2",
                Description = "2",
                StreamingService = "HBO Max"
            }).ToUrl("/api/shows");
           // _.StatusCodeShouldBe(400);
        });



    }

}
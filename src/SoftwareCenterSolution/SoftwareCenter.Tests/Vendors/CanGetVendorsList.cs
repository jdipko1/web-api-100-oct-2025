using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Alba;
using Microsoft.OpenApi.Models;

namespace SoftwareCenter.Tests.Vendors;
public class CanGetVendorsList
{
    [Fact]
    public async Task GivesASuccessStatusCodeAsync()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5282");
        var response = await client.GetAsync("/vendors");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GettingAllVendorsAsync()
    {
        //start up the api in memory using my program.cs
        var host = await AlbaHost.For<Program>();
        await host.Scenario(api =>
        {
            api.Get.Url("/vendors");
            //if it isn't this fail
            api.StatusCodeShouldBeOk();
        }); 
    }
}
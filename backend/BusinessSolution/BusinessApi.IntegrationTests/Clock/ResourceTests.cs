

using Alba;
using System.Net;

namespace BusinessApi.IntegrationTests.Clock;
public class ResourceTests
{

    [Fact]
    public async Task GivesMeA200()
    {
        var host = await AlbaHost.For<Program>();

        await host.Scenario(api =>
        {
            api.Get.Url("/clock");
            api.StatusCodeShouldBeOk();
        });
    }

    [Fact]
    public async Task DuringOpenHours()
    {
        var expected = new GetClockResponse(true, null);

        var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/clock");
        });

        var responseMessage = response.ReadAsJson<GetClockResponse>();

        Assert.Equal(expected, responseMessage);
    }
    [Fact]
    public async Task DuringClosedHours()
    {
        // This will fail on saturdays, sundays, before 9 and after 5
        var expected = new GetClockResponse(false, null);

        var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/clock");
        });

        var responseMessage = response.ReadAsJson<GetClockResponse>();

        //.Assert.Equal(expected, responseMessage);
        Assert.False(responseMessage.IsOpen);
    }
}

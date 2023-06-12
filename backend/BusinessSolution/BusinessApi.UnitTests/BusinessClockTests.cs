
using BusinessApi.Adapters;
using BusinessApi.Models;
using BusinessApi.Services;

namespace BusinessApi.UnitTests;
public class BusinessClockTests
{
    [Fact]
    public void ClosedOnSaturday()
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrentLocalTime()).Returns(new DateTime(2023, 6, 10));
        var clock = new BusinessClock(stubbedClock.Object);

        var response = clock.GetClockResponse(); // SUT! 

        var expected = new GetClockResponse(false, new DateTime(2023, 6, 12, 9, 0, 0));
        Assert.Equal(expected, response);
    }

    [Fact]
    public void ClosedOnSunday()
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrentLocalTime()).Returns(new DateTime(2023, 6, 11));
        var clock = new BusinessClock(stubbedClock.Object);

        var response = clock.GetClockResponse();

        var expected = new GetClockResponse(false, new DateTime(2023, 6, 12, 9, 0, 0));
        Assert.Equal(expected, response);
    }

    [Theory]
    [InlineData("6/12/2023 9:00:00 AM")]
    [InlineData("6/12/2023 4:59:59 PM")]

    public void OpenTimes(string dateTime)
    {

        var systemTime = new Mock<ISystemTime>();
        systemTime.Setup(t => t.GetCurrentLocalTime()).Returns(DateTime.Parse(dateTime));

        var clock = new BusinessClock(systemTime.Object);

        var response = clock.GetClockResponse();
        Assert.True(response.IsOpen);
        Assert.Null(response.NextOpenTime);
    }

    [Theory]
    [InlineData("6/12/2023 8:59:59 AM")]
    [InlineData("6/12/2023 5:00:00 AM")]
    public void ClosedTimes(string dateTime)
    {
        var systemTime = new Mock<ISystemTime>();
        systemTime.Setup(t => t.GetCurrentLocalTime()).Returns(DateTime.Parse(dateTime));

        var clock = new BusinessClock(systemTime.Object);

        var response = clock.GetClockResponse();
        Assert.False(response.IsOpen);
        Assert.NotNull(response.NextOpenTime);
    }
}


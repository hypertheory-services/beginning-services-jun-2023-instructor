

using Moq;
using OnCallDeveloperApi.Services;

namespace OnCallDeveloperApi.UnitTests;

public class HolidayBasedSupportScheduleTests
{
    [Fact]
    public void NoInHouseSupportOnWeekends()
    {
        var stubbedSystemTime = new Mock<ISystemTime>();
        stubbedSystemTime.Setup(s => s.GetCurrent()).Returns(new DateTime(2023, 4, 1)); // Saturday
        var supportSchedule = new HolidayBasedSupportSchedule(stubbedSystemTime.Object);

        Assert.False(supportSchedule.InternalSupportAvailable);
    }
    [Fact]
    public void InHouseSupportOnWeekDays()
    {
        var stubbedSystemTime = new Mock<ISystemTime>();

        stubbedSystemTime.Setup(s => s.GetCurrent()).Returns(new DateTime(2023, 3, 28)); // Tuesday
        var supportSchedule = new HolidayBasedSupportSchedule(stubbedSystemTime.Object);


        Assert.True(supportSchedule.InternalSupportAvailable);
    }

    [Fact]
    public void NoSupportOnChristmas()
    {
        var stubbedSystemTime = new Mock<ISystemTime>();

        stubbedSystemTime.Setup(s => s.GetCurrent()).Returns(new DateTime(2020, 12, 25)); // Christmas
        var supportSchedule = new HolidayBasedSupportSchedule(stubbedSystemTime.Object);


        Assert.False(supportSchedule.InternalSupportAvailable);
    }
}

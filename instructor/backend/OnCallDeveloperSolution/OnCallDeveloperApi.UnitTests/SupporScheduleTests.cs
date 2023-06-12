
using Moq;
using OnCallDeveloperApi.Services;

namespace OnCallDeveloperApi.UnitTests;

public class SupporScheduleTests
{
    [Fact]
    public void NoInHouseSupportOnWeekends()
    {
        var stubbedSystemTime = new Mock<ISystemTime>();
        stubbedSystemTime.Setup(s => s.GetCurrent()).Returns(new DateTime(2023, 4,1)); // Saturday
        var supportSchedule = new SupportSchedule(stubbedSystemTime.Object);

        Assert.False(supportSchedule.InternalSupportAvailable);
    }
    [Fact]
    public void InHouseSupportOnWeekDays()
    {
        var stubbedSystemTime = new Mock<ISystemTime>();
       
        stubbedSystemTime.Setup(s => s.GetCurrent()).Returns(new DateTime(2023, 3, 28)); // Tuesday
        var supportSchedule = new SupportSchedule(stubbedSystemTime.Object);
    

        Assert.True(supportSchedule.InternalSupportAvailable);
    }
}

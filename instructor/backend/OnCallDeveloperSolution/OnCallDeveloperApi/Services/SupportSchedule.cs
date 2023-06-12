namespace OnCallDeveloperApi.Services;

public class SupportSchedule : IProvideSupportSchedule
{
    private readonly ISystemTime _systemTime;

    public SupportSchedule(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }

    public bool InternalSupportAvailable
    {
        get
        {
            var now = _systemTime.GetCurrent();
            return !(now.DayOfWeek == DayOfWeek.Sunday || now.DayOfWeek == DayOfWeek.Saturday);
        }
    }
}

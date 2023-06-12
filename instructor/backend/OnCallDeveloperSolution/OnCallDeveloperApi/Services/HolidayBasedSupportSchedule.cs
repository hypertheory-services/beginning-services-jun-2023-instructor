namespace OnCallDeveloperApi.Services;

public class HolidayBasedSupportSchedule : IProvideSupportSchedule
{
    private readonly ISystemTime _systemTime;

    public HolidayBasedSupportSchedule(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }

    public bool InternalSupportAvailable
    {
        get
        {
            var now = _systemTime.GetCurrent();
            if(now.Day == 25 && now.Month == 12)
            {
                return false;
            }
            return !(now.DayOfWeek == DayOfWeek.Sunday || now.DayOfWeek == DayOfWeek.Saturday);
        }
    }
}



namespace BusinessApi.Services;

public class BusinessClock
{
    private readonly ISystemTime _clock;

    public BusinessClock(ISystemTime clock)
    {
        _clock = clock;
    }

    public GetClockResponse GetClockResponse()
    {
        var now = _clock.GetCurrentLocalTime();
        var dayOfWeek = now.DayOfWeek;

        var hour = now.Hour;

        var openingTime = new TimeSpan(9, 0, 0);
        var closingTime = new TimeSpan(17, 0, 0);

        var isOpen = dayOfWeek switch
        {
            DayOfWeek.Saturday => false,
            DayOfWeek.Sunday => false,
            _ => hour >= openingTime.Hours && hour < closingTime.Hours,
        };
        if(isOpen)
        {
            return new GetClockResponse(true, null);
        }
        // Pattern Matching Switch

        var openingNext = dayOfWeek switch
        {
            DayOfWeek.Saturday => now.AddDays(2),
            DayOfWeek.Sunday => now.AddDays(1),
            _ => now.AddDays(1)
        };

        openingNext = openingNext.Date.Add(openingTime);

        return new GetClockResponse(false, openingNext);

    }
}

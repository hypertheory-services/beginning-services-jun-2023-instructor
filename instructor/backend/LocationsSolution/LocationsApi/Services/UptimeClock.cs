namespace LocationsApi.Services;

public class UptimeClock
{
    private readonly DateTime _startTime;

    public UptimeClock()
    {
        _startTime = DateTime.Now;
    }

    public DateTime UpSince
    {
        get
        {
            return _startTime;
        }
    }
}

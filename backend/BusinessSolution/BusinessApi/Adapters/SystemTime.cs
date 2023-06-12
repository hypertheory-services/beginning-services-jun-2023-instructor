namespace BusinessApi.Adapters;

public interface ISystemTime
{
    DateTime GetCurrentLocalTime();
}

public class SystemTime : ISystemTime
{
    public DateTime GetCurrentLocalTime() { return DateTime.Now; }
}

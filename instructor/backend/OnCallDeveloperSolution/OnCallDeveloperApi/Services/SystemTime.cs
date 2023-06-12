namespace OnCallDeveloperApi.Services;

public interface ISystemTime
{
    DateTime GetCurrent();
}

public class SystemTime : ISystemTime
{
   // public DateTime GetCurrent() => DateTime.Now;
   public DateTime GetCurrent()
    {
        return DateTime.Now;
    }
}

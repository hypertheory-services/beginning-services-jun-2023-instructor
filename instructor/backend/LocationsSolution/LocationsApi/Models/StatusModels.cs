namespace LocationsApi.Models;


public class GetStatusResponse
{
    public ContactInfo? ContactInfo { get; set; } = new();
    public Uptime Uptime { get; set; } = new();

}

public class ContactInfo
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}

public class Uptime
{
    public int Days { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public int Milliseconds { get; set; }
}

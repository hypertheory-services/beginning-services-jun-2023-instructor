using LocationsApi.Adapters;
using LocationsApi.Models;
using LocationsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocationsApi.Controllers;

public class StatusController : ControllerBase
{
    private readonly UptimeClock _clock;
    private readonly OnCallDeveloperHttpAdapter _adapter;

    public StatusController(UptimeClock clock, OnCallDeveloperHttpAdapter adapter)
    {
        _clock = clock;
        _adapter = adapter;
    }



    // GET /status
    [HttpGet("/support")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    public async Task<ActionResult> GetStatus()
    {
        var sinceStartup =  DateTime.Now - _clock.UpSince;
        ContactInfo? contactInfo;
        try
        {
            contactInfo = await _adapter.GetContactInfoAsync();
        }
        catch (Exception)
        {

            //contactInfo = new ContactInfo
            //{
            //    Name = "Front Desk",
            //    Email = "frontdesk@company.com",
            //    Phone = "888-2828"
            //};
            contactInfo = null;
        }
        var response = new GetStatusResponse()
        {
               ContactInfo = contactInfo,
               Uptime = new Uptime
               {
                   Hours = sinceStartup.Hours,
                   Minutes = sinceStartup.Minutes,
                   Days = sinceStartup.Days,
                   Seconds = sinceStartup.Seconds,
                   Milliseconds = sinceStartup.Milliseconds
               }
        };
        return Ok(response);
    }
}

using IssueTrackerApi.Adapters;
using IssueTrackerApi.Models;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;

namespace IssueTrackerApi.Controllers;

[ApiController]
public class IssuesController : ControllerBase
{
    private readonly IDocumentStore _documentStore;
    private readonly BusinessApiAdapter _businessApi;


    public IssuesController(IDocumentStore documentStore, BusinessApiAdapter businessApi)
    {
        _documentStore = documentStore;
        _businessApi = businessApi;
    }

    [HttpGet("/open-issues")]
    public async Task<ActionResult> GetOpenIssues()
    {
        using var session = _documentStore.LightweightSession();
        var data = await session.Query<IssueCreatedResponse>().Where(issue => issue.ClosedAt == null).ToListAsync();

        return Ok(new { issues = data });
    }

    [HttpPost("/open-issues")]
    public async Task<ActionResult> AddAnIssue([FromBody] IssueCreateRequest request)
    {
        // Validate it. if invalid, return a 400.
        //if(!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}
        // If it's good, create an issueresponse
        // Save it to the database
        // send them a copy of it.

        var response = new IssueCreatedResponse
        {
            Id = Guid.NewGuid(),
            Issue = request.Issue,
            From = request.From,
            CreatedAt = DateTime.UtcNow,
        };

        using var session = _documentStore.LightweightSession();
        session.Insert(response);
        await session.SaveChangesAsync();
        bool circuitIsBroken = false;
        ClockResponseModel? supportInfo = null;
        try
        {
             supportInfo = await _businessApi.GetClockResponseAsync();
        }
        catch (BrokenCircuitException)
        {

            circuitIsBroken = true;
        }
        IssueCreatedResponseWithSupportInfo actualResponse;
        if (supportInfo is null)
        {
            actualResponse = new IssueCreatedResponseWithSupportInfo
            {
                Issue = response,
                Support = circuitIsBroken ? new SupportModel
                {
                    IsOpenNow = false,
                    OpensAt = null,
                    SupportNumber = "Unavailable Now - Sorry"

                } : new()
            };
        }
        else
        {

            actualResponse = new IssueCreatedResponseWithSupportInfo
            {
                Issue = response,
                Support = new SupportModel
                {
                    IsOpenNow = supportInfo.IsOpen,
                    OpensAt = supportInfo.IsOpen ? null : supportInfo.NextOpenTime,
                    SupportNumber = "(800) 555-5555"
                }
            };


           
        }
        return Ok(actualResponse);
    }
}

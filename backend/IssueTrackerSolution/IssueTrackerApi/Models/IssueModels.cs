namespace IssueTrackerApi.Models;

public record IssueCreateRequest
{
    public string From { get; init; } = string.Empty;
    public string Issue { get; init; } = string.Empty;
}


public record IssueCreatedResponse
{
    public Guid Id { get; init; } = Guid.Empty;
    public string From { get; init; } = string.Empty;
    public string Issue { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? ClosedAt { get; init; }

}
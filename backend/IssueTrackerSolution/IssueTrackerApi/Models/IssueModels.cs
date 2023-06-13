﻿using System.ComponentModel.DataAnnotations;

namespace IssueTrackerApi.Models;

public record IssueCreateRequest
{
    [Required, MaxLength(20)]
    public string From { get; init; } = string.Empty;
    [Required]
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
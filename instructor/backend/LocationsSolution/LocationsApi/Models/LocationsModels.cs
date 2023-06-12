using System.ComponentModel.DataAnnotations;

namespace LocationsApi.Models;


public record LocationsResponse
{
    public IReadOnlyList<LocationItemResponse>? _embedded { get; set; } 
}
public record LocationItemResponse
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AddedBy { get; init; } = string.Empty;
    public DateTime AddedOn { get; init; }
}

public record LocationCreate
{
    [Required, MaxLength(75)]
    public string Name { get; init; } = string.Empty;
    [Required, MaxLength(1000)]
    public string Description { get; init; } = string.Empty;

}
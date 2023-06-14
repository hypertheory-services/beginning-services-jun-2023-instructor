namespace IssueTrackerApi.Adapters;

// This will be the adapter we use whenever we call in to the BusinessApi
public class BusinessApiAdapter
{
    private readonly HttpClient _httpClient;

    public BusinessApiAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ClockResponseModel?> GetClockResponseAsync()
    {
        var response = await _httpClient.GetAsync("/clock");
        response.EnsureSuccessStatusCode(); // 200 - 299

        var model = await response.Content.ReadFromJsonAsync<ClockResponseModel>();


        return model;

    }

}

public record ClockResponseModel
{
    public bool IsOpen { get; init; }
    public DateTime? NextOpenTime { get; init; }
}
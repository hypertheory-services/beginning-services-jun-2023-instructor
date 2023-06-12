using LocationsApi.Models;

namespace LocationsApi.Adapters;

public class OnCallDeveloperHttpAdapter
{
    private readonly HttpClient _httpClient;

    public OnCallDeveloperHttpAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ContactInfo?> GetContactInfoAsync()
    {
        var response = await _httpClient.GetAsync("/oncalldeveloper");
        response.EnsureSuccessStatusCode();

        var contact = await response.Content.ReadFromJsonAsync<ContactInfo?>();

        return contact;
    }

}

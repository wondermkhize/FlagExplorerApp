using System.Net;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;

namespace Infrastructure.Data;

public class RestCountriesClient : IRestCountriesClient
{
    private readonly HttpClient _httpClient;

    public RestCountriesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAllCountriesAsync()
    {
        return await _httpClient.GetStringAsync("all");
    }

    public async Task<string> GetCountryByNameAsync(string name)
    {
        return await GetStringOrThrowAsync($"name/{name}",
            notFoundMessage: $"Country '{name}' not found.");
    }

    private async Task<string> GetStringOrThrowAsync(string uri, string? notFoundMessage = null)
    {
        var response = await _httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        if (response.StatusCode == HttpStatusCode.NotFound && notFoundMessage != null)
        {
            throw new CountryNotFoundException(notFoundMessage);
        }

        throw new ExternalApiException(
            $"Request to '{uri}' failed with status code {(int)response.StatusCode} ({response.StatusCode}).");
    }
}


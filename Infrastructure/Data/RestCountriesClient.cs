using System.Net;
using System.Net.Http.Headers;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;

namespace Infrastructure.Data;

public class RestCountriesClient : IRestCountriesClient
{
    private readonly HttpClient _httpClient;

    public RestCountriesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetAllCountriesAsync()
    {
        return await GetStringOrThrowAsync("countries/v5?limit=100");
    }

    public async Task<string> GetCountryByNameAsync(string name)
    {
        return await GetStringOrThrowAsync($"countries/v5/name/{name}",
            notFoundMessage: $"Country '{name}' not found.");
    }

    private async Task<string> GetStringOrThrowAsync(string uri, string? notFoundMessage = null)
    {
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.GetAsync(uri);
        }
        catch (HttpRequestException ex)
        {
            throw new ExternalApiException($"Request to '{uri}' failed because the external API was unavailable.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new ExternalApiException($"Request to '{uri}' timed out.", ex);
        }

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


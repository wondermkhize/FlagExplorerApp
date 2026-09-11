using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly IRestCountriesClient _apiClient;

    public CountryService(IRestCountriesClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        var response = await _apiClient.GetAllCountriesAsync();

        var countries = DeserializeCountries(response);

        return countries.Select(c => new Country
        {
            Name = c.Names?.Common,
            Flag = c.Flag?.UrlPng
        });
    }

    public async Task<CountryDetails> GetCountryDetailsAsync(string name)
    {
        var response = await _apiClient.GetCountryByNameAsync(name);

        var country = DeserializeCountries(response).FirstOrDefault() ?? throw new CountryNotFoundException($"Country '{name}' not found.");

        return new CountryDetails
        {
            Name = country.Names?.Common,
            Flag = country.Flag?.UrlPng,
            Population = country.Population,
            Capital = country.Capitals?.FirstOrDefault()?.Name
        };
    }

    private List<RestCountryApiResponse> DeserializeCountries(string json)
    {
        var token = JToken.Parse(json);

        if (token is JArray array)
        {
            return array.ToObject<List<RestCountryApiResponse>>() ?? [];
        }

        if (token is JObject obj)
        {
            var errors = obj["errors"] as JArray;
            var message = errors?
                .Select(e => e["message"]?.ToString())
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(message))
            {
                throw new ExternalApiException(message);
            }

            var dataToken = obj["data"];

            if (dataToken is JArray dataArray)
            {
                return dataArray.ToObject<List<RestCountryApiResponse>>() ?? [];
            }

            if (dataToken is JObject dataObject && dataObject["objects"] is JArray objectsArray)
            {
                return objectsArray.ToObject<List<RestCountryApiResponse>>() ?? [];
            }
        }

        return [];
    }
}

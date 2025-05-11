using Newtonsoft.Json;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Exceptions;

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
            Name = c.name.common,
            Flag = c.flags.png
        });
    }

    public async Task<CountryDetails> GetCountryDetailsAsync(string name)
    {
        var response = await _apiClient.GetCountryByNameAsync(name);

        var country = DeserializeCountries(response).FirstOrDefault() ?? throw new CountryNotFoundException($"Country '{name}' not found.");

        return new CountryDetails
        {
            Name = country?.name.common,
            Flag = country?.flags.png,
            Population = country?.population,
            Capital = country?.capital[0]
        };
    }

    private List<dynamic> DeserializeCountries(string json)
    {
        return JsonConvert.DeserializeObject<List<dynamic>>(json) ?? [];
    }
}

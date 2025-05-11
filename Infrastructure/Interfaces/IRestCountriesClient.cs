namespace Infrastructure.Interfaces;

public interface IRestCountriesClient
{
    Task<string> GetAllCountriesAsync();

    Task<string> GetCountryByNameAsync(string name);
}

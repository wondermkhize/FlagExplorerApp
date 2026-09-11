using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Infrastructure.Interfaces;
using Infrastructure.Data;
using System.Net;
using Infrastructure.Services;
using Infrastructure.Exceptions;
using Middleware;
using API.Controllers;

namespace BackEndTests.Integration
{
    public class CountryEndpointsTests : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;

        public CountryEndpointsTests(TestServerFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GetCountries_ReturnsOk()
        {
            // Arrange
            var url = "/api/countries";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetCountries_ReturnsNotFound_WhenCountryDoesNotExist()
        {
            // Arrange
            var url = "/api/countries/nonexistentcountry";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    public class TestServerFixture
    {
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IRestCountriesClient, FakeRestCountriesClient>();
                    services.AddScoped<ICountryService, CountryService>();
                    services.AddEndpointsApiExplorer();
                    services.AddControllers()
                        .AddApplicationPart(typeof(CountriesController).Assembly);
                    services.AddSwaggerGen();
                })
                .Configure(app =>
                {
                    app.UseCustomExceptionHandler();
                    app.UseCors("AllowAll");
                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });

            var server = new TestServer(webHostBuilder);
            Client = server.CreateClient();
        }
    }

    public class FakeRestCountriesClient : IRestCountriesClient
    {
        public Task<string> GetAllCountriesAsync()
        {
            return Task.FromResult("""
                [
                    {
                        "name": { "common": "South Africa" },
                        "flags": { "png": "za.svg" },
                        "population": 60000000,
                        "capital": ["Pretoria"]
                    }
                ]
                """);
        }

        public Task<string> GetCountryByNameAsync(string name)
        {
            if (string.Equals(name, "nonexistentcountry", StringComparison.OrdinalIgnoreCase))
            {
                throw new CountryNotFoundException($"Country '{name}' not found.");
            }

            return Task.FromResult("""
                [
                    {
                        "name": { "common": "South Africa" },
                        "flags": { "png": "za.svg" },
                        "population": 60000000,
                        "capital": ["Pretoria"]
                    }
                ]
                """);
        }
    }
}

using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Infrastructure.Interfaces;
using Infrastructure.Data;
using System.Net;
using Infrastructure.Services;

namespace BackEndTests.Integration
{
    public class CountryEndpointsTests : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;

        public CountryEndpointsTests(TestServerFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact(Skip = "Skipping this test temporarily due to pending implementation.")]
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
            // Configure the TestServer using Program.cs
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHttpClient<IRestCountriesClient, RestCountriesClient>(client =>
                    {
                        client.BaseAddress = new Uri("http://localhost");
                    });

                    services.AddScoped<ICountryService, CountryService>();

                    services.AddEndpointsApiExplorer();

                    services.AddControllers();

                    services.AddSwaggerGen();
                })
                .Configure(app =>
                {
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
}

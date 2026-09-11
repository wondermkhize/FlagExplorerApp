using FluentAssertions;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Moq;

namespace BackEndTests.Unit
{
    public class CountryServiceTests
    {
        private readonly Mock<IRestCountriesClient> _mockClient;

        private readonly CountryService _service;

        public CountryServiceTests()
        {
            _mockClient = new Mock<IRestCountriesClient>();
            _service = new CountryService(_mockClient.Object);
        }

        [Fact]
        public async Task GetCountryDetailsAsync_WithValidCountryName_ReturnsMappedCountryDetails()
        {
            // Arrange
            var countryName = "South Africa";

            var apiJsonResponse = """
                {
                  "data": {
                    "objects": [
                      {
                        "names": { "common": "South Africa" },
                        "flag": { "url_png": "https://flagcdn.com/w320/za.png" },
                        "population": 60000000,
                        "capitals": [
                          { "name": "Pretoria" }
                        ]
                      }
                    ]
                  }
                }
                """;

            var expected = new CountryDetails
            {
                Name = "South Africa",
                Flag = "https://flagcdn.com/w320/za.png",
                Population = 60000000,
                Capital = "Pretoria"
            };

            _mockClient.Setup(x => x.GetCountryByNameAsync(countryName))
                       .ReturnsAsync(apiJsonResponse);

            // Act
            var result = await _service.GetCountryDetailsAsync(countryName);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCountryDetailsAsync_WhenApiReturnsEmptyArray_ThrowsCountryNotFoundException()
        {
            // Arrange
            var countryName = "UnknownCountry";
            var emptyApiResponse = "[]";

            _mockClient.Setup(x => x.GetCountryByNameAsync(countryName))
                       .ReturnsAsync(emptyApiResponse);

            // Act & Assert
            await Assert.ThrowsAsync<CountryNotFoundException>(() =>
                _service.GetCountryDetailsAsync(countryName));
        }

        [Fact]
        public async Task GetAllCountriesAsync_WhenApiReturnsCountries_ReturnsExpectedCountryList()
        {
            // Arrange
            var apiResponse = """
                {
                  "data": {
                    "objects": [
                      {
                        "names": { "common": "Botswana" },
                        "flag": { "url_png": "https://flagcdn.com/w320/bw.png" }
                      },
                      {
                        "names": { "common": "Germany" },
                        "flag": { "url_png": "https://flagcdn.com/w320/de.png" }
                      }
                    ]
                  }
                }
                """;

            var expected = new List<Country>
            {
                new Country { Name = "Botswana", Flag = "https://flagcdn.com/w320/bw.png" },
                new Country { Name = "Germany", Flag = "https://flagcdn.com/w320/de.png" }
            };

            _mockClient.Setup(x => x.GetAllCountriesAsync())
                       .ReturnsAsync(apiResponse);

            // Act
            var result = await _service.GetAllCountriesAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task GetAllCountriesAsync_WhenApiReturnsEmptyArray_ReturnsEmptyList()
        {
            // Arrange
            var emptyApiResponse = "[]";

            _mockClient.Setup(x => x.GetAllCountriesAsync())
                       .ReturnsAsync(emptyApiResponse);

            // Act
            var result = await _service.GetAllCountriesAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCountriesAsync_WhenApiReturnsWrappedErrorResponse_ThrowsExternalApiException()
        {
            // Arrange
            var wrappedErrorResponse = """
                {
                  "success": false,
                  "data": null,
                  "errors": [
                    {
                      "message": "This API version has been deprecated. Please visit https://restcountries.com/docs/countries/legacy-api-deprecation to migrate to our new version (v5)."
                    }
                  ]
                }
                """;

            _mockClient.Setup(x => x.GetAllCountriesAsync())
                       .ReturnsAsync(wrappedErrorResponse);

            // Act
            var act = async () => await _service.GetAllCountriesAsync();

            // Assert
            await act.Should().ThrowAsync<ExternalApiException>()
                .WithMessage("*deprecated*new version (v5)*");
        }

        [Fact]
        public async Task GetAllCountriesAsync_WhenApiReturnsV5ObjectsPayload_ReturnsExpectedCountryList()
        {
            // Arrange
            var apiResponse = """
                {
                  "data": {
                    "objects": [
                      {
                        "names": { "common": "Botswana" },
                        "flag": { "url_png": "https://flagcdn.com/w320/bw.png" },
                        "population": 2300000,
                        "capitals": [
                          { "name": "Gaborone" }
                        ]
                      }
                    ]
                  }
                }
                """;

            _mockClient.Setup(x => x.GetAllCountriesAsync())
                       .ReturnsAsync(apiResponse);

            // Act
            var result = await _service.GetAllCountriesAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<Country>
            {
                new Country
                {
                    Name = "Botswana",
                    Flag = "https://flagcdn.com/w320/bw.png"
                }
            });
        }

    }
}
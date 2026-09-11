using Newtonsoft.Json;

namespace Infrastructure.Models;

public class RestCountryApiResponse
{
    [JsonProperty("names")]
    public RestCountryNames? Names { get; set; }

    [JsonProperty("flag")]
    public RestCountryFlag? Flag { get; set; }

    [JsonProperty("population")]
    public int Population { get; set; }

    [JsonProperty("capitals")]
    public List<RestCountryCapital>? Capitals { get; set; }
}

public class RestCountryNames
{
    [JsonProperty("common")]
    public string? Common { get; set; }
}

public class RestCountryFlag
{
    [JsonProperty("url_png")]
    public string? UrlPng { get; set; }
}

public class RestCountryCapital
{
    [JsonProperty("name")]
    public string? Name { get; set; }
}

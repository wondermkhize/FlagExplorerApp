using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Middleware;

var builder = WebApplication.CreateBuilder(args);

var restCountriesBaseUrl = builder.Configuration["RestCountriesApi:BaseUrl"];
if (string.IsNullOrWhiteSpace(restCountriesBaseUrl))
{
    throw new InvalidOperationException("RestCountriesApi:BaseUrl is not configured.");
}

var restCountriesApiKey = builder.Configuration["RestCountriesApi:ApiKey"];

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? ["http://localhost:5173"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddHttpClient<IRestCountriesClient, RestCountriesClient>(client =>
{
    client.BaseAddress = new Uri(restCountriesBaseUrl);

    if (!string.IsNullOrWhiteSpace(restCountriesApiKey))
    {
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", restCountriesApiKey);
    }
});

builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowConfiguredOrigins");

app.MapControllers();

app.Run();

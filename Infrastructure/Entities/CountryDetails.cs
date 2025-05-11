using System;

namespace Infrastructure.Entities;

public class CountryDetails : Country
{
    public int Population { get; set; }
    public string? Capital { get; set; }
}

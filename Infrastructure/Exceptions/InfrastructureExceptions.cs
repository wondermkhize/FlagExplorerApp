using System;

namespace Infrastructure.Exceptions;

public class CountryNotFoundException : Exception
{
    public CountryNotFoundException(string message) : base(message) { }
}

public class ExternalApiException : Exception
{
    public ExternalApiException(string message) : base(message) { }
}

using System;

namespace WeatherApp.Extensions
{
    public static class StringExtensions
    {
        public static string FirstLetterUppercase(this string value)
        => value switch
        {
            null => throw new ArgumentNullException(nameof(value)),
            "" => throw new ArgumentException($"{nameof(value)} cannot be empty", nameof(value)),
            _ => string.Concat(value[0].ToString().ToUpperInvariant(), value.AsSpan(1))
        };
}
}

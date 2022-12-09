using Microsoft.Extensions.Primitives;

namespace WeatherApp.Components
{
    public class LanguageViewModel
    {
        public LanguageViewModel(string path, IQueryCollection query)
        {
            this.CurrentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "fr" ? "EN" : "FR";
            this.Url = $"{path}?{string.Join("&", GetQueryString(query))}";
        }

        private static IEnumerable<string> GetQueryString(IQueryCollection query)
        {
            var queryWithoutCulture = query.Where(q => q.Key != "culture");

            if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName != "fr")
                return GetQueryWithoutCulture(queryWithoutCulture);

            return GetQueryWithoutCulture(queryWithoutCulture)
                .Concat(new[] { "culture=en-US" });
        }

        private static IEnumerable<string> GetQueryWithoutCulture(IEnumerable<KeyValuePair<string, StringValues>> query)
            => query.Select(q => $"{q.Key}={q.Value.First()}");

        public string CurrentLanguage { get; }

        public string Url { get; }

    }
}

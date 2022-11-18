using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Components
{
    [ViewComponent]
    public class LanguageViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new LanguageViewModel(this.Request.Path.Value, this.Request.Query));
        }
    }

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
                return queryWithoutCulture.Select(q => q.Value.First());

            return queryWithoutCulture.Select(q => $"{q.Key}={q.Value.First()}")
                .Concat(new[] { "culture=en-US" });
        }

        public string CurrentLanguage { get; }

        public string Url { get; }

    }
}

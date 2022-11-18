using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WeatherApp.Translations
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("a")]
    public class CultureTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var queryString = this.GetQueryString(output);

            if (queryString == null)
                return;

            queryString = $"{queryString}{GetQueryCharacter(queryString)}culture={Thread.CurrentThread.CurrentCulture.Name}";

            output.Attributes.SetAttribute("href", queryString);
        }

        private string GetQueryString(TagHelperOutput output)
        {
            if (Thread.CurrentThread.CurrentCulture.Name.Contains("fr"))
                return null;

            if (!output.Attributes.TryGetAttribute("href", out var href))
                return null;

            return href.Value as string;
        }

        private static string GetQueryCharacter(string queryString)
            => queryString.Contains("?") ? "&" : "?";
    }
}

using Microsoft.Extensions.Localization;
using System.Reflection;

namespace WeatherApp.Translations
{
    internal class ResxTranslationProvider : ITranslationProvider
    {
        private readonly IStringLocalizer localizer;       

        public ResxTranslationProvider(IStringLocalizerFactory factory)
        {
            this.localizer = factory.CreateStringLocalizer();
        }

        public bool CanTranslate(string key)
        {
            var cui = Thread.CurrentThread.CurrentUICulture;
            var c = Thread.CurrentThread.CurrentCulture;
            return key.StartsWith(TranslationKeys.Prefix) && !this.GetLocalizedString(key).ResourceNotFound;
        }

        public string Translate(string key)
        {
            return this.GetLocalizedString(key).Value;
        }

        private LocalizedString GetLocalizedString(string key)
            => this.localizer[key.Replace(TranslationKeys.Prefix, string.Empty)];
    }
}

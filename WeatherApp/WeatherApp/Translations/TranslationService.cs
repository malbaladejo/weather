namespace WeatherApp.Translations
{
    internal class TranslationService: ITranslationService
    {
        private readonly IEnumerable<ITranslationProvider> translationProviders;

        public TranslationService(IEnumerable<ITranslationProvider> translationProviders)
        {
            this.translationProviders = translationProviders;
        }

        public string Translate(string key)
        {
            var label = this.translationProviders
                .Where(p => p.CanTranslate(key))
                .Select(p=>p.Translate(key)).FirstOrDefault();

            return label ?? key;
        }
    }
}

namespace WeatherApp.Translations
{
    public interface ITranslationProvider
    {
        bool CanTranslate(string key);
        string Translate(string key);
    }
}

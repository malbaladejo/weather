using WeatherApp.ViewModels.DateContexts;

namespace WeatherApp.ViewModels
{
    public interface IDateContextFactory
    {
        DateContext Create();
    }
}

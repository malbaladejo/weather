namespace WeatherApp.ViewModels.DateContexts
{
    public interface IDateContextFactoryStratregy
    {
        IDateContextFactory Create(Period period);
    }
}

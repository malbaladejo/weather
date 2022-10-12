namespace WeatherApp.ViewModels
{
    public class ControllerActionContext
    {
        public ControllerActionContext(string controller, string action = null)
        {
            Controller = controller;
            Action = action;
        }

        public string Controller { get; }
        public string Action { get; }
    }
}

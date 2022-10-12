using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Extensions
{
    public static class ControllerUtilities
    {
        public static string GetName<T>() where T : Controller
            => typeof(T).Name.Replace(nameof(Controller), string.Empty).ToLowerInvariant();

        public static string GetAction(string actionName) => actionName.ToLowerInvariant();
    }
}

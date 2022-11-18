using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace WeatherApp.Translations
{
    public static class LocalizationExtensions
    {
        public static IStringLocalizer CreateStringLocalizer(this IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            return factory.Create(nameof(SharedResource), assemblyName.Name);
        }

        public static IMvcBuilder AddMvcLocalizations(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddViewLocalization()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.CreateStringLocalizer();
                };
            });

            return mvcBuilder;
        }

        public static IServiceCollection AddServicesLocalizations(this IServiceCollection services)
        {
            services.AddSingleton<ITranslationProvider, ResxTranslationProvider>();
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            return services;
        }

        public static WebApplication AddWepApplicationLocalizations(this WebApplication app, string defaultRequestCulture, params string[] otherCultres)
        {
            var options = new RequestLocalizationOptions();

            var supportedCultures = new List<CultureInfo>(otherCultres.Concat(new[] { defaultRequestCulture }).Select(c=> new CultureInfo(c)));

            options.DefaultRequestCulture = new RequestCulture(defaultRequestCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders.Clear();
            options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());

            app.UseRequestLocalization(options);

            return app;
        }
    }
}

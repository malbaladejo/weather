using System.Text;

namespace WeatherApp
{
	public static class ExceptionExtensions
    {
        public static string DumpAsString(this Exception e)
        {
            var builder = new StringBuilder();

            builder.AppendLine(e.GetType().FullName);
            builder.AppendLine(e.Message);

            if (e.InnerException != null)
            {
                builder.AppendLine();
                builder.AppendLine(e.InnerException.DumpAsString());

            }

            return builder.ToString();

        }
    }
}

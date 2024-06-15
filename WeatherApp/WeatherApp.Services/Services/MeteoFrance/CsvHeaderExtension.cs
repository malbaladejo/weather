using System.Linq.Expressions;
using System.Reflection;
namespace WeatherApp.Services
{
	public static class CsvHeaderExtension
	{
		public static decimal GetDecimal<T>(this IReadOnlyList<string> data, IReadOnlyDictionary<string, int> headers, string propertyName)
			=> data.GetValue<T>(headers, propertyName).ParseDecimal();

		public static DateTime GetDate<T>(this IReadOnlyList<string> data, IReadOnlyDictionary<string, int> headers, string propertyName)
			=> data.GetValue<T>(headers, propertyName).ParseDate();
		public static string GetValue<T>(this IReadOnlyList<string> data, IReadOnlyDictionary<string, int> headers, string propertyName)
		{
			var headerName = typeof(T).GetHeaderName(propertyName);
			var headerIndex = headers[headerName];
			return data[headerIndex];
		}

		public static string GetHeaderName(this Type type, string propertyName)
			=> type.GetPropertyInfo(propertyName)?.GetCustomAttribute<CsvHeaderAttribute>()?.Name;

		public static string GetHeaderName<TSource, TProperty>(this Expression<Func<TSource, TProperty>> expression)
		{
			var csvHeader = expression.GetPropertyInfo().GetCustomAttribute<CsvHeaderAttribute>();
			return csvHeader?.Name;
		}
		public static PropertyInfo GetPropertyInfo(this Type type, string propertyName) => type.GetProperty(propertyName);

		public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyLambda)
		{
			if (propertyLambda.Body is not MemberExpression member)
			{
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda.ToString()));
			}

			if (member.Member is not PropertyInfo propInfo)
			{
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda.ToString()));
			}

			Type type = typeof(TSource);
			if (propInfo.ReflectedType != null && type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
			{
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a property that is not from type {1}.",
					propertyLambda.ToString(),
					type));
			}

			return propInfo;
		}

		public static DateTime ParseDate(this string value)
		{
			if (value.Length == 8)
				return new DateTime(int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)));
			if (value.Length == 10)
				return new DateTime(int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)), int.Parse(value.Substring(8, 2)), 0, 0);

			throw new Exception($"Incorrect date format: {value}");
		}

		public static decimal ParseDecimal(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return 0;

			if (decimal.TryParse(value, out var doubleValue))
				return doubleValue;

			throw new Exception($"Incorrect double format: {value}");
		}
	}
}

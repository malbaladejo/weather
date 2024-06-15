namespace WeatherApp.Services
{
	[AttributeUsage(AttributeTargets.Property)]
	public class CsvHeaderAttribute : Attribute
	{
		public CsvHeaderAttribute(string name)
		{
			this.Name = name;
		}

		public string Name { get; }
	}
}

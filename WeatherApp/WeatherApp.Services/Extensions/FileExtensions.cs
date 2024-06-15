namespace WeatherApp.Extensions
{
	public static class FileExtensions
	{
		public static string[] ReadAllLines(this string filePath)
		{
			using (var csv = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (var sr = new StreamReader(csv))
				{
					var file = new List<string>();
					while (!sr.EndOfStream)
						file.Add(sr.ReadLine());

					return file.ToArray();
				}
			}
		}
	}
}

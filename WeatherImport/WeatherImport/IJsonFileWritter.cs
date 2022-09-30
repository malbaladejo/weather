using System.Collections.Generic;

namespace WeatherImport
{
    internal interface IJsonFileWritter
    {
        void Write(string outFolder, IReadOnlyCollection<DayDto> data);
    }
}

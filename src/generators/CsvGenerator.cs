using System.Globalization;
using CsvHelper;

namespace gasmie.src
{
    public static class CsvGenerator
    {
        public static void Generate(List<object> dto)
        {
            var path = Path.Combine(
                Environment.CurrentDirectory
                , $"src/files/{dto.First()}"
                , $"{DateTime.Now.ToFileTime()}.csv");

            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(dto);
        }
    }
}
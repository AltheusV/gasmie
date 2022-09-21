using System.Globalization;
using CsvHelper;

namespace gasmie.src
{
    public static class CsvGenerator
    {
        public static void Generate(List<object> dto)
        {
            using var writer = new StreamWriter(CreatePath(dto.First()));
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(dto);
        }

        private static string CreatePath(object dto)
        {
            var path = $"files/{dto}";
            Directory.CreateDirectory(path);
            return Path.Combine(path, $"{DateTime.Now.ToFileTime()}.csv");
        }
    }
}
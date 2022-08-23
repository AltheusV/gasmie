using System.Globalization;
using CsvHelper;

namespace gasmie.src
{
    public static class CsvGenerator
    {
        public static void Generate(List<Object> dto)
        {
            using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "src/files", $"{DateTime.Now.ToFileTime()}.csv")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(dto);
            }
        }
    }
}
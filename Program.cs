using gasmie.settings;
using gasmie.src;
using gasmie.src.notion;


var (url, id, key) = Settings.GetNotionConnectionStrings();
var dtos = await NotionRequest.GetScraperDtos(url, id, key);
var scrapers = ScraperGenerator.Generate(dtos);
var listScrapers = scrapers.GroupBy(s => s.ToString()).ToList();
var listDtos = listScrapers.Select(l => l.Select(s => s.Dig()).ToList()).ToList();
listDtos.ForEach(l => CsvGenerator.Generate(l));




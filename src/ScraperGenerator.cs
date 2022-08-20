namespace gasmie.src
{
    public class ScraperGenerator
    {
        private ScraperGenerator() { }
        public static List<Scraper> Init(List<ScraperDto> dtos)
        {
            return dtos.Select(dto => Init(dto)).ToList();
        }

        public static Scraper Init(ScraperDto dto)
        {
            return dto.Mode switch
            {
                ScraperMode.ANIME => new StreamingScraper(dto.Url, "Anime"),
                ScraperMode.GAME => new GameScraper(dto.Url),
                ScraperMode.MOVIE => new StreamingScraper(dto.Url, "Movie"),
                ScraperMode.TVSERIES => new StreamingScraper(dto.Url, "TV Series"),
                _ => throw new ArgumentException("Invalid Mode"),
            };
        }
    }
}
namespace gasmie.src
{
    public class ScraperDto
    {
        public string Url { get; init; }
        public ScraperMode Mode { get; init; }

        public ScraperDto(string url, ScraperMode mode)
        {
            Url = url;
            Mode = mode;
        }
    }
}
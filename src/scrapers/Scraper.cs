using HtmlAgilityPack;

namespace gasmie.src
{
    public abstract class Scraper
    {
        public HtmlDocument Document { get; init; }
        public string URL { get; init; }
        public Scraper(string url)
        {
            HtmlWeb web = new();
            URL = url;
            Document = web.Load(url);
        }

        public abstract object Dig();
    }
}
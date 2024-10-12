using gasmie.src.data;

namespace gasmie.src
{
    public class StreamingScraper : Scraper
    {
        public string Type { get; init; }

        public StreamingScraper(string url, string type) : base(url)
        {
            Type = type;
        }

        public override object Dig()
        {
            return new StreamingDto(
                Type, 
                DigImage(), 
                DigName(), 
                DigEpisodes(), 
                DigGenres(), 
                DigDuration(), 
                URL);
        }

        private string DigImage()
        {
            var imageNode = Document.DocumentNode.SelectNodes("//span[@class='title-poster title-poster--no-radius-bottom']/picture[@class='picture-comp title-poster__image']/img");
            return imageNode is null ? "" : $"{imageNode[0].OuterHtml.Split(" ")[3].Replace("\"", "").Replace("data-src=", "")}.jpg";
        }

        private string DigName()
        {
            var nameNode = Document.DocumentNode.SelectNodes("//div[@class='title-block']/div/h1");
            return nameNode is null ? "" : nameNode[0].InnerText.Trim();
        }

        private string DigEpisodes()
        {
            if (Type.Equals("Movie"))
                return "";

            var episodesNode = Document.DocumentNode.SelectNodes("//div[@class='detail-infos__subheading']/h2[@class='detail-infos__subheading--label']");
            return episodesNode is null ? "" : episodesNode.Where(e => e.InnerHtml.Contains("Episodes")).First().InnerHtml.Trim();
        }

        private string DigGenres()
        {
            var genresNode = Document.DocumentNode.SelectNodes("//div[@class='title-info']/div[@class='detail-infos']/div[@class='detail-infos__value']");
            return genresNode is null ? "" : genresNode[1].InnerText.Trim().Replace(" ,", ",").Replace("&amp;", "&");
        }

        private string DigDuration()
        {
            var durationNode = Document.DocumentNode.SelectNodes("//div[@class='title-info visible-xs visible-sm']/div[@class='detail-infos']/div[@class='detail-infos__value']");
            return durationNode is null ? "" : durationNode[2].InnerText.Trim();
        }
    }
}
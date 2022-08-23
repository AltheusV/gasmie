using HtmlAgilityPack;

namespace gasmie.src
{
    public class GameScraper : Scraper
    {
        public GameScraper(string url) : base(url) { }

        public override object Dig()
        {
            return new GameDto(
                "Library",
                DigName(),
                DigImage(),
                DigDuration(1),
                DigDuration(2),
                DigGenres(),
                DigDevelopers(),
                DigRelease(),
                URL);
        }

        private string DigImage()
        {
            var imageNode = Document.DocumentNode.SelectNodes("//div[@class='game_image mobile_hide']/img");
            return $"https://howlongtobeat.com{imageNode.First().OuterHtml.Split()[3].Split("'")[1]}";
        }

        private string DigName()
        {
            var nameNode = Document.DocumentNode.SelectNodes("//div[@class='profile_header shadow_text']");
            return nameNode.First().InnerText.Replace("\n", "").Replace("\t", "");
        }

        private string DigDuration(int duration)
        {
            var mainDurationNode = Document.DocumentNode.SelectNodes("//div[@class='game_times']/ul/li[@class='short time_100']/div");
            return mainDurationNode[duration].InnerText.Replace("&#189;", "½").Replace("\t", "").Trim();
        }

        private string DigGenres()
        {
            var genresNode = DigNode(Document.DocumentNode.SelectNodes("//div[@class='profile_info medium']"), "Genres");
            return genresNode.Any() ? genresNode.First().InnerText.Replace("Genres:", "").Replace("\n", "").Replace("\t", "").Trim() : "";
        }

        private string DigDevelopers()
        {
            var developersNode = DigNode(Document.DocumentNode.SelectNodes("//div[@class='profile_info medium']"), "Developer");
            return developersNode.Any() ? developersNode.First().InnerText.Replace("Developers:", "").Replace("Developer:", "").Replace("\n", "").Replace("\t", "").Trim() : "";
        }

        private string DigRelease()
        {
            var developersNode = Document.DocumentNode.SelectNodes("//div[@class='profile_info']");
            return developersNode.First().InnerText.Replace("\n", "").Replace("\t", "").Split(":")[1].Trim();
        }

        private static IEnumerable<HtmlNode> DigNode(HtmlNodeCollection nodes, string keyword)
        {
            return nodes.Where(e => e.InnerText.Contains(keyword));
        }
    }
}
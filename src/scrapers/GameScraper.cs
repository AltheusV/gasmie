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
                DigDuration("Main + Extras"),
                DigDuration("Completionist"),
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

        private string DigDuration(string keyword)
        {
            var nodes = Document.DocumentNode.SelectNodes("//div[@class='game_times']/ul/li");
            var mainDurationNode = nodes.FirstOrDefault(n => n.InnerHtml.Contains(keyword));
            return mainDurationNode == null ? "" : mainDurationNode.InnerText.Replace(keyword, "").Replace("&#189;", "½").Replace("\t", "").Trim();
        }

        private string DigGenres()
        {
            var genresNode = DigNode(Document.DocumentNode.SelectNodes("//div[@class='profile_info medium']"), "Genres");
            return genresNode == null ? "" : genresNode.InnerText.Replace("Genres:", "").Replace("\n", "").Replace("\t", "").Trim();
        }

        private string DigDevelopers()
        {
            var developersNode = DigNode(Document.DocumentNode.SelectNodes("//div[@class='profile_info medium']"), "Developer");
            return developersNode == null ? "" : developersNode.InnerText.Replace("Developers:", "").Replace("Developer:", "").Replace("\n", "").Replace("\t", "").Trim();
        }

        private string DigRelease()
        {
            var developersNode = Document.DocumentNode.SelectNodes("//div[@class='profile_info']");
            return developersNode.First().InnerText.Replace("\n", "").Replace("\t", "").Split(":")[1].Trim();
        }

        private static HtmlNode? DigNode(HtmlNodeCollection nodes, string keyword)
        {
            return nodes.FirstOrDefault(e => e.InnerText.Contains(keyword));
        }
    }
}
using HtmlAgilityPack;

namespace gasmie.src
{
    public class GameScraper : Scraper
    {
        private const string NAME_AND_IMAGE_NODE = "//div[@class='GameSideBar_game_image__pMeFK mobile_hide']/img";
        private const string DURATION_NODE = "//div[@class='GameStats_game_times__5LFEc']/ul/li";
        private const string GENRES_AND_DEVELOPERS_NODE = "//div[@class='GameSummary_profile_info__e935c GameSummary_medium__5cP8Y']";
        private const string RELEASE_NODE = "//div[@class='GameSummary_profile_info__e935c']";

        public GameScraper(string url) : base(url) { }

        public override object Dig()
        {
            var nameAndImage = DigNameAndImage();

            return new GameDto(
                "Library",
                nameAndImage[0],
                nameAndImage[1],
                DigDuration("Main + Sides"),
                DigDuration("Completionist"),
                DigGenres(),
                DigDevelopers(),
                DigRelease(),
                URL);
        }

        private string[] DigNameAndImage()
        {
            string[] nameAndImage = { "", "" };
            
            var nameAndImageNode = Document.DocumentNode.SelectNodes(NAME_AND_IMAGE_NODE);
            if (nameAndImageNode is not null)
            {
                var outerHtml = nameAndImageNode.First().OuterHtml.Split("\"");
                nameAndImage[0] = FormatString(outerHtml[1], new string[] { "Box Art" }).Replace("&#x27;", "'");
                nameAndImage[1] = outerHtml[3];
            }
            return nameAndImage;
        }

        private string DigDuration(string keyword)
        {
            var nodes = Document.DocumentNode.SelectNodes(DURATION_NODE);
            var durationNode = nodes.FirstOrDefault(n => n.InnerHtml.Contains(keyword));
            return durationNode is null ? "" : FormatString(durationNode.InnerText, new string[] { keyword, "\t" }).Replace("&#189;", "½");
        }

        private string DigGenres()
        {
            var genresNode = DigNodeByKeyword(Document.DocumentNode.SelectNodes(GENRES_AND_DEVELOPERS_NODE), "Genre");
            return genresNode is null ? "" : FormatString(genresNode.InnerText, new string[] { "Genres:", "Genre:", "\n", "\t" });
        }

        private string DigDevelopers()
        {
            var developersNode = DigNodeByKeyword(Document.DocumentNode.SelectNodes(GENRES_AND_DEVELOPERS_NODE), "Developer");
            return developersNode is null ? "" : FormatString(developersNode.InnerText, new string[] { "Developers:", "Developer:", "\n", "\t" });
        }

        private string DigRelease()
        {
            var releaseNode = Document.DocumentNode.SelectNodes(RELEASE_NODE);
            return releaseNode is null ? "" : FormatString(releaseNode.First().InnerText, new string[] { "\n", "\t" }).Split(":")[1].Trim();
        }

        private static HtmlNode? DigNodeByKeyword(HtmlNodeCollection nodes, string keyword)
        {
            return nodes.FirstOrDefault(n => n.InnerText.Contains(keyword));
        }

        private static string FormatString(string value, string[] strings)
        {
            foreach (var str in strings)
            {
                value = value.Replace(str, "");
            }

            return value.Trim();
        }
    }
}
namespace gasmie.src.data
{
    public class StreamingDto

    {
        public string Type { get; init; }
        public string Name { get; init; }
        public string Image { get; init; }
        public string Status { get; init; }
        public string Genres { get; init; }
        public string Duration { get; init; }
        public string Episodes { get; set; }
        public string URL { get; set; }

        public StreamingDto(
            string type,
            string image,
            string name,
            string episodes,
            string genres,
            string duration,
            string url)
        {
            Status = "To Watch";
            Type = type;
            Image = image;
            Name = name;
            Episodes = episodes;
            Genres = genres;
            Duration = duration;
            URL = url;
        }
    }
}
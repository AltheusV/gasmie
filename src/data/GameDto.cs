namespace gasmie.src
{
    public class GameDto
    {
        public string Status { get; init; }
        public string Name { get; init; }
        public string Image { get; init; }
        public string MainDuration { get; init; }
        public string CompletionistDuration { get; init; }
        public string Genres { get; init; }
        public string Developers { get; init; }
        public string Release { get; init; }
        public string URL { get; init; }

        public GameDto(
            string status,
            string name,
            string image,
            string mainDuration,
            string completionistDuration,
            string genres,
            string developers,
            string release,
            string url)
        {
            Status = status;
            Name = name;
            Image = image;
            MainDuration = mainDuration;
            CompletionistDuration = completionistDuration;
            Genres = genres;
            Developers = developers;
            Release = release;
            URL = url;
        }
        
        public override string ToString()
        {
            return "games";
        }
    }
}
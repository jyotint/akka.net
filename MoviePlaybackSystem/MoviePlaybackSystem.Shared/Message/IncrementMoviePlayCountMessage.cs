namespace MoviePlaybackSystem.Shared.Message
{
    public class IncrementMoviePlayCountMessage
    {
        public string MessageId { get; private set; }
        public string MovieTitle { get; private set; }
        public int Count { get; private set; }

        public IncrementMoviePlayCountMessage(string movieTitle, int count)
        {
            MessageId = System.Guid.NewGuid().ToString();
            MovieTitle = movieTitle;
            Count = count;
        }
    }
}

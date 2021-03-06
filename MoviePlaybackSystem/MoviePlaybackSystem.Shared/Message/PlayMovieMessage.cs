namespace MoviePlaybackSystem.Shared.Message
{
    // Action-008: Create PlayMovieMessage class
    public class PlayMovieMessage
    {
        public string MessageId { get; private set; }
        public string MovieTitle { get; private set; }
        public int UserId { get; private set; }

        public PlayMovieMessage(string movietitle, int userId)
        {
            MessageId = System.Guid.NewGuid().ToString();
            MovieTitle = movietitle;
            UserId = userId;
        }

        override public string ToString()
        {
            return $"[{MessageId}] UserId: {UserId}, MovieTitle: '{MovieTitle}'";
        }

    }
}

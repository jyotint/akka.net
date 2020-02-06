namespace MoviePlaybackSystem.Shared.Message
{
    public class StopMovieMessage
    {
        public string MessageId { get; private set; }
        public int UserId { get; private set; }

        public StopMovieMessage(int userId)
        {
            MessageId = System.Guid.NewGuid().ToString();
            UserId = userId;
        }

        override public string ToString()
        {
            return $"[{MessageId}] UserId: {UserId}";
        }
    }
}

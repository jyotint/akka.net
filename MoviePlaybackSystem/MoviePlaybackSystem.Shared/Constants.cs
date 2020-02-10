namespace MoviePlaybackSystem.Shared
{
    public static class Constants
    {
        public static readonly string ActorSystemName = "MovieStreamingActorSystem";

        public static readonly string ActorNameMoviePlaybackActor = "MoviePlaybackActor";

        public static readonly string ActorNamePlaybackStatisticsActor = "PlaybackStatisticsActor";
        public static readonly string ActorNameMoviePlayCounterActor = "MoviePlayCounterActor";

        public static readonly string ActorNameUserCoordinatorActor = "UserCoordinatorActor";
        public static readonly string ActorNameUserActorPrefix = "UserActor";

        public static readonly char CommandSeparator = ',';
        public static readonly string PathSeparator = "/";
        public static readonly string AkkaConfigurationFileName = "akka.hocon";
    }
}

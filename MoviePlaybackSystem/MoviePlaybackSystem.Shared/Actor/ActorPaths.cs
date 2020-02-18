using Akka.Actor;
using MoviePlaybackSystem.Shared;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    /// <summary>
    /// Static helper class used to define paths to fixed-name actors
    /// (helps eliminate errors when using <see cref="ActorSelection"/>)
    /// </summary>
    public static class ActorPaths
    {
        public static readonly ActorMetaData MoviePlaybackActor = new ActorMetaData(Constants.ActorNameMoviePlaybackActor);
        
        public static readonly ActorMetaData PlaybackStatisticsActor = new ActorMetaData(Constants.ActorNamePlaybackStatisticsActor, MoviePlaybackActor);
        public static readonly ActorMetaData MoviePlayCounterActor = new ActorMetaData(Constants.ActorNameMoviePlayCounterActor, PlaybackStatisticsActor);

        public static readonly ActorMetaData UserCoordinatorActor = new ActorMetaData(Constants.ActorNameUserCoordinatorActor, MoviePlaybackActor);

        public static ActorMetaData GetUserActorMetaData(string userId)
        {
            return new ActorMetaData(Constants.ActorNameUserActorPrefix, UserCoordinatorActor, "-" + userId);
        }

        public static void LogAllActorPaths()
        {
            ColoredConsole.WriteTitle($"Logging all actors paths: ");
            ColoredConsole.WriteTitle($"  MoviePlaybackActor: {ActorPaths.MoviePlaybackActor.Path}");
            ColoredConsole.WriteTitle($"  PlaybackStatisticsActor: {ActorPaths.PlaybackStatisticsActor.Path}");
            ColoredConsole.WriteTitle($"  MoviePlayCounterActor: {ActorPaths.MoviePlayCounterActor.Path}");
            ColoredConsole.WriteTitle($"  UserCoordinatorActor: {ActorPaths.UserCoordinatorActor.Path}");
            string exampleUserId = "999";
            ColoredConsole.WriteTitle($"  UserActor({exampleUserId}): {ActorPaths.GetUserActorMetaData(exampleUserId).Path}");
        }
    }
}

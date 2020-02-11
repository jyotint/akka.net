using Akka.Actor;
using MoviePlaybackSystem.Shared;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;

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
    }
}

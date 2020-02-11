using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    public class MoviePlaybackActor : CustomUntypedActor
    {
        private ActorSystemHelper _actorSystemHelper;
        public ActorHelper UserCoordinatorActorHelper { get; private set; }
        public ActorHelper PlaybackStatisticsActorHelper { get; private set; }

        public MoviePlaybackActor(ActorSystemHelper actorSystemHelper)
            : base()
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");
            _actorSystemHelper = actorSystemHelper;

            UserCoordinatorActorHelper = _actorSystemHelper.CreateActorHelper(Context, UserCoordinatorActor.Props(_actorSystemHelper), ActorPaths.UserCoordinatorActor.Name);
            PlaybackStatisticsActorHelper = _actorSystemHelper.CreateActorHelper(Context, PlaybackStatisticsActor.Props(_actorSystemHelper), ActorPaths.PlaybackStatisticsActor.Name);
        }

        public static Akka.Actor.IActorRef Create(ActorSystemHelper actorSystemHelper)
        {
            return actorSystemHelper.CreateActor(MoviePlaybackActor.Props(actorSystemHelper), ActorPaths.MoviePlaybackActor.Name);
        }
        public static ActorHelper CreateActorHelper(ActorSystemHelper actorSystemHelper)
        {
            return new ActorHelper(Create(actorSystemHelper));
        }

        public static Akka.Actor.Props Props(ActorSystemHelper actorSystemHelper)
        {
            return Akka.Actor.Props.Create(() => new MoviePlaybackActor(actorSystemHelper));
        }

        override protected void OnReceive(object message)
        {
            // No message processing required as of now
        }
    }
}

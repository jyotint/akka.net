using Akka.Actor;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    public class MoviePlaybackActor : CustomUntypedActor
    {
        public IActorRef UserCoordinatorActorRef { get; private set; }
        public IActorRef PlaybackStatisticsActorRef { get; private set; }

        public MoviePlaybackActor()
            : base()
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");

            UserCoordinatorActorRef = ActorSystemHelper.CreateActorHelper(Context, UserCoordinatorActor.Props(), ActorPaths.UserCoordinatorActor.Name);
            PlaybackStatisticsActorRef = ActorSystemHelper.CreateActorHelper(Context, PlaybackStatisticsActor.Props(), ActorPaths.PlaybackStatisticsActor.Name);
        }

        public static Akka.Actor.IActorRef Create()
        {
            return ActorSystemHelper.CreateActor(MoviePlaybackActor.Props(), ActorPaths.MoviePlaybackActor.Name);
        }
        public static Akka.Actor.Props Props()
        {
            return Akka.Actor.Props.Create(() => new MoviePlaybackActor());
        }

        override protected void OnReceive(object message)
        {
            // No message processing required as of now
        }
    }
}

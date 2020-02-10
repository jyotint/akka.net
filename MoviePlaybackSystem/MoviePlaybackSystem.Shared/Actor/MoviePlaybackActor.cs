using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    public class MoviePlaybackActor : CustomUntypedActor
    {
        private ActorSystemHelper _actorSystemHelper;

        public MoviePlaybackActor(ActorSystemHelper actorSystemHelper)
            : base()
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");
            _actorSystemHelper = actorSystemHelper;

            _actorSystemHelper.CreateActor(Context, UserCoordinatorActor.Props(_actorSystemHelper), UserCoordinatorActor.GetActorName());
            _actorSystemHelper.CreateActor(Context, PlaybackStatisticsActor.Props(_actorSystemHelper), PlaybackStatisticsActor.GetActorName());
        }

        public static string GetActorName()
        {
            return Constants.ActorNameMoviePlaybackActor;
        }

        override protected void OnReceive(object message)
        {
            // No message processing required as of now
        }
    }
}

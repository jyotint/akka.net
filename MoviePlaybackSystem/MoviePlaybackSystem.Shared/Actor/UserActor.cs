using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    // Action-003 - Create an untyped actor
    public class UserActor : CustomUntypedActor
    {
        private int _userId;
        private PlayMovieMessage _currentlyPlaying;

        public UserActor(int userId)
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");
            _userId = userId;
            _currentlyPlaying = null;

            // Initial behavior
            Become(StoppedBehavior);
        }

        public static string GetActorName(int userId)
        {
            return Constants.ActorNameUserActorPrefix + userId; //"-" + userId;
        }

        public static Akka.Actor.IActorRef Create(int userId)
        {
            return Context.ActorOf(UserActor.Props(userId), GetActorName(userId));
        }

        public static Akka.Actor.Props Props(int userId)
        {
            return Akka.Actor.Props.Create<UserActor>(() => new UserActor(userId));
        }

        protected override void OnReceive(object message)
        {
            ColoredConsole.WriteReceivedMessage($"  [{this.ActorName}] OnReceive(): Received Message on '{this.ActorName}' Actor...");

            switch(message)
            {
                case PlayMovieMessage pmm:
                    Become(PlayingMovieBehavior);
                    break;
                case StopMovieMessage smm:
                    Become(StoppedBehavior);
                    break;
                default:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] OnReceive(): ERROR: Unknown '{message.GetType().ToString()}' type received!");
                    Unhandled(message);
                    break;
            }
        }

        private void PlayingMovieBehavior(object message)
        {
            switch(message)
            {
                case PlayMovieMessage pmm:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] ERROR: Already playing movie! PlayMovieMessage message received --> {pmm.ToString()}.");
                    break;
                case StopMovieMessage smm:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] StopMovieMessage message received --> {smm.ToString()}.");
                    StopPlayingMovie(smm);
                    break;
            }
        }

        private void StoppedBehavior(object message)
        {
            switch(message)
            {
                case PlayMovieMessage pmm:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] PlayMovieMessage message received --> {pmm.ToString()}.");
                    StartPlayingMovie(pmm);
                    break;
                case StopMovieMessage smm:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] ERROR: Can't stop if nothing is playing! StopMovieMessage message received --> {smm.ToString()}.");
                    break;
            }
        }

        private void StartPlayingMovie(PlayMovieMessage pmm)
        {
            _currentlyPlaying = pmm;
            Become(PlayingMovieBehavior);
        }

        private void StopPlayingMovie(StopMovieMessage smm)
        {
            _currentlyPlaying = null;
            Become(StoppedBehavior);
        }
    }
}

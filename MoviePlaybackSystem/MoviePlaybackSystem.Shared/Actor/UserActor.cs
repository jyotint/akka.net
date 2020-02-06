using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    // Action-003 - Create an untyped actor
    public class UserActor : CustomUntypedActor
    {
        private PlayMovieMessage _currentlyPlaying;

        public UserActor()
        {
            _currentlyPlaying = null;
            ColoredConsole.WriteCreationEvent($"CREATED '{ActorName}' Actor.");

            // Initial behavior
            Become(StoppedBehavior);
        }

        public static Akka.Actor.Props Props()
        {
            return Akka.Actor.Props.Create<UserActor>(() => new UserActor());
        }

        protected override void OnReceive(object message)
        {
            ColoredConsole.WriteReceivedMessage($"  Received Message on '{Constants.ActorNameUserActor}' Actor...");

            if(message is PlayMovieMessage pmm)
            {
                Become(PlayingMovieBehavior);
            }
            else if(message is StopMovieMessage smm)
            {
                Become(StoppedBehavior);
            }
            else
            {
                ColoredConsole.WriteReceivedMessage($"    OnReceive(): ERROR: Unknown '{message.GetType().ToString()}' type received!");
                Unhandled(message);
            }
        }

        private void PlayingMovieBehavior(object message)
        {
            if(message is PlayMovieMessage pmm)
            {
                ColoredConsole.WriteReceivedMessage($"    ERROR: Already playing movie! PlayMovieMessage message received --> {pmm.ToString()}.");
            }
            else if(message is StopMovieMessage smm)
            {
                ColoredConsole.WriteReceivedMessage($"    StopMovieMessage message received --> {smm.ToString()}.");
                StopPlayingMovie(message as StopMovieMessage);
            }
        }

        private void StoppedBehavior(object message)
        {
            if(message is PlayMovieMessage pmm)
            {
                ColoredConsole.WriteReceivedMessage($"    PlayMovieMessage message received --> {pmm.ToString()}.");
                StartPlayingMovie(message as PlayMovieMessage);
            }
            else if(message is StopMovieMessage smm)
            {
                ColoredConsole.WriteReceivedMessage($"    ERROR: Can't stop if nothing is playing! StopMovieMessage message received --> {smm.ToString()}.");
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

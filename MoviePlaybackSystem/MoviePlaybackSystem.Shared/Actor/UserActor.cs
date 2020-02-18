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

        public static Akka.Actor.IActorRef Create(int userId)
        {
            return Context.ActorOf(UserActor.Props(userId), ActorPaths.GetUserActorMetaData(userId.ToString()).Name);
        }

        public static Akka.Actor.Props Props(int userId)
        {
            return Akka.Actor.Props.Create<UserActor>(() => new UserActor(userId));
        }

        protected override void OnReceive(object message)
        {
            ColoredConsole.WriteReceivedMessage($"  [{this.ActorName}] OnReceive(): Received Message on '{this.ActorName}' Actor...");

            switch (message)
            {
                case PlayMovieMessage msg:
                    Become(PlayingMovieBehavior);
                    break;
                case StopMovieMessage msg:
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
            switch (message)
            {
                case PlayMovieMessage msg:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] ERROR: Already playing movie! PlayMovieMessage message received --> {msg.ToString()}.");
                    break;
                case StopMovieMessage msg:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] StopMovieMessage message received --> {msg.ToString()}.");
                    StopPlayingMovie(msg);
                    break;
            }
        }

        private void StoppedBehavior(object message)
        {
            switch (message)
            {
                case PlayMovieMessage msg:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] PlayMovieMessage message received --> {message.ToString()}.");
                    StartPlayingMovie(msg);
                    break;
                case StopMovieMessage msg:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] ERROR: Can't stop if nothing is playing! StopMovieMessage message received --> {msg.ToString()}.");
                    break;
            }
        }

        private void StartPlayingMovie(PlayMovieMessage message)
        {
            _currentlyPlaying = message;
            
            // Context.ActorSelection(ActorPaths.MoviePlayCounterActor.Path).Tell(new IncrementMoviePlayCountMessage(message.MovieTitle, 1));
            var actorRef = ActorSystemHelper.GetActorRefUsingResolveOne(ActorPaths.MoviePlayCounterActor.Path);
            if (actorRef != null)
            {
                ActorSystemHelper.SendAsynchronousMessage(actorRef, new IncrementMoviePlayCountMessage(message.MovieTitle, 1));
            }

            Become(PlayingMovieBehavior);
        }

        private void StopPlayingMovie(StopMovieMessage message)
        {
            _currentlyPlaying = null;
            Become(StoppedBehavior);
        }
    }
}

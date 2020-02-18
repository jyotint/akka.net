using Akka.Actor;
using MoviePlaybackSystem.Shared.Actor;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Helpers
{
    public static class MoviePlaybackSystemHelper
    {
        public static string ActorSystemName { get; private set; }

        private static IActorRef _moviePlaybackActorRef;
        private static IActorRef _userCoordinatorActorRef;

        public static void StartActorSystem(bool createRootActor = false)
        {
            ActorSystemName = Constants.ActorSystemName;

            // Create an ActionSystem
            ColoredConsole.WriteTitle($"Creating '{ActorSystemName}' ActorSystem...");

            // Create an Akka ActorSystem
            ActorSystemHelper.CreateActorSystem(ActorSystemName);

            if (createRootActor == true)
            {
                // Create first 'user' actor 'MoviePlaybackActor' in Akka ActorSystem
                _moviePlaybackActorRef = ActorSystemHelper.CreateActor(MoviePlaybackActor.Props(), ActorPaths.MoviePlaybackActor.Name);
            }
        }

        public static void TerminateActorSystem()
        {
            ColoredConsole.WriteTitle("  Quitting...");

            // Terminate an ActorSystem
            ActorSystemHelper.TerminateActorSystem();
        }

        private static IActorRef GetUserCoordinatorActorRef()
        {
            if (_userCoordinatorActorRef == null)
            {
                // Get reference to UserCoordinatorActor for sending PlayMovieMessage and StopMovieMessage
                _userCoordinatorActorRef = ActorSystemHelper.GetActorRefUsingResolveOne(ActorPaths.UserCoordinatorActor.Path);
            }

            return _userCoordinatorActorRef;
        }

        public static int StartPlayingMovie(PlayMovieMessage message)
        {
            ActorSystemHelper.SendAsynchronousMessage(GetUserCoordinatorActorRef(), message);
            return 0;
        }

        public static int StopPlayingMovie(StopMovieMessage message)
        {
            ActorSystemHelper.SendAsynchronousMessage(GetUserCoordinatorActorRef(), message);
            return 0;
        }
    }
}

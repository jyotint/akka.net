using MoviePlaybackSystem.Shared;
using MoviePlaybackSystem.Shared.Actor;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.ConsoleUI
{
    public class MoviePlaybackSystemHelper
    {
        private ActorSystemHelper _actorSystemHelper;
        private ActorHelper _userCoordinatorActorHelper;

        public MoviePlaybackSystemHelper()
        {
        }

        public void StartActorSystem()
        {
            // Create an ActionSystem
            ColoredConsole.WriteTitle($"Creating '{Constants.ActorSystemName}' ActorSystem...");

            // Create an Akka ActorSystem
            _actorSystemHelper = new ActorSystemHelper();
            _actorSystemHelper.CreateActorSystem(Constants.ActorSystemName);

            // Create first 'user' actor 'MoviePlaybackActor' in Akka ActorSystem
            ActorHelper moviePlaybackActorHelper = _actorSystemHelper.CreateActorHelper(MoviePlaybackActor.Props(_actorSystemHelper), ActorPaths.MoviePlaybackActor.Name);

            // Get reference to UserCoordinatorActor for sending PlayMovieMessage and StopMovieMessage
            _userCoordinatorActorHelper = new ActorHelper(_actorSystemHelper.GetActorRefUsingResolveOne(ActorPaths.UserCoordinatorActor.Path));
        }

        public void TerminateActorSystem()
        {
            ColoredConsole.WriteTitle("  Quitting...");

            if(_actorSystemHelper != null)
            {
                // Terminate an ActorSystem
                _actorSystemHelper.TerminateActorSystem();
                _actorSystemHelper = null;
            }
        }

        public int StartPlayingMovie(CommandParser.StartMovieOptions options)
        {
            _userCoordinatorActorHelper.SendMessageAsynchronous(new PlayMovieMessage(options.MovieTitle, options.UserId));
            return 0;
        }

        public int StopPlayingMovie(CommandParser.StopMovieOptions options)
        {
            _userCoordinatorActorHelper.SendMessageAsynchronous(new StopMovieMessage(options.UserId));
            return 0;
        }
    }
}

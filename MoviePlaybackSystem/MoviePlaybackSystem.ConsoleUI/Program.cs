using System;
using MoviePlaybackSystem.Shared;
using MoviePlaybackSystem.Shared.Actor;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var actorSystem = new ActorSystemHelper();

                ColoredConsole.WriteTitle("MoviePlaybackSystem: Running Akka.NET demo...");

                // Create an ActionSystem
                ColoredConsole.WriteTitle($"Creating '{Constants.ActorSystemName}' ActorSystem...");
                actorSystem.CreateActorSystem(Constants.ActorSystemName);

                RunTests(actorSystem);

                // Terminate an ActorSystem
                Console.WriteLine("  Press ENTER key to terminate ActorSystem...");
                Console.ReadLine();
                actorSystem.TerminateActorSystem();

                // Wait for user input to terminate the application
                ColoredConsole.WriteUserPrompt("  Press ENTER key to close the application...");
                ColoredConsole.WriteUserPrompt("");
            }
            catch (Exception ex)
            {
                ColoredConsole.WriteError($"Exception occurred (type: '{ex.GetType().Name}')");
                ColoredConsole.WriteError(ex.Message);
                ColoredConsole.WriteError(ex.StackTrace);
            }
        }

        private static void RunTests(ActorSystemHelper actorSystemHelper)
        {
            ActorHelper userCoordinatorActorHelper = actorSystemHelper.CreateActorHelper(UserCoordinatorActor.Props(actorSystemHelper), UserCoordinatorActor.GetActorName());

            // Send PlayMovieMessage to PlaybackActor
            userCoordinatorActorHelper.SendMessageAsynchronous(new PlayMovieMessage("Blood Diamond", 42));
            userCoordinatorActorHelper.SendMessageAsynchronous(new PlayMovieMessage("The Departed", 51));
            userCoordinatorActorHelper.SendMessageAsynchronous(new PlayMovieMessage("Terminator", 42));

            // Send StopMovieMessage to UserActor
            userCoordinatorActorHelper.SendMessageAsynchronous(new StopMovieMessage(42));
            userCoordinatorActorHelper.SendMessageAsynchronous(new StopMovieMessage(51));

            // Send PlayMovieMessage to PlaybackActor
            userCoordinatorActorHelper.SendMessageAsynchronous(new PlayMovieMessage("Usual Suspect", 42));
        }
    }
}

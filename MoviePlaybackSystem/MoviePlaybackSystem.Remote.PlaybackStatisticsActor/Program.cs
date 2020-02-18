using System;
using MoviePlaybackSystem.Shared.Actor;
using MoviePlaybackSystem.Shared.Helpers;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Remote.PlaybackStatisticsActor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ColoredConsole.WriteTitle("MoviePlaybackSystem.Remote.PlaybackStatisticsActor: Running Akka.NET demo...");

                // Start ActorSystem
                MoviePlaybackSystemHelper.StartActorSystem(false);
                ActorPaths.LogAllActorPaths();


                // Wait for user input to terminate the application
                ColoredConsole.WriteUserPrompt($"  Press ENTER key to terminate '{MoviePlaybackSystemHelper.ActorSystemName}' ActorSystem...");
                Console.ReadLine();

                // Terminate ActorSystem
                // MoviePlaybackSystemHelper.TerminateActorSystem();
 
                ColoredConsole.WriteTitle("MoviePlaybackSystem.Remote.PlaybackStatisticsActor: Quitting Akka.NET demo.");
            }
            catch (Exception ex)
            {
                ColoredConsole.WriteError($"MoviePlaybackSystem.Remote.PlaybackStatisticsActor: Exception occurred (type: '{ex.GetType().Name}')");
                ColoredConsole.WriteError(ex.Message);
                ColoredConsole.WriteError(ex.StackTrace);
            }
            finally
            { 
                // Terminate ActorSystem
                MoviePlaybackSystemHelper.TerminateActorSystem();
            }
        }
    }
}

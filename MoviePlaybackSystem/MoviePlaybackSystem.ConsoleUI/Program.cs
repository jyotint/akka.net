using System;
using System.Threading.Tasks;
using Akka.Actor;
using MoviePlaybackSystem.Shared;
using MoviePlaybackSystem.Shared.Actor;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        static void Main(string[] args)
        {
            ColoredConsole.WriteTitle("MoviePlaybackSystem: Running Akka.NET demo...");

            // Create an ActionSystem
            _movieStreamingActorSystem = ActorSystem.Create(Constants.ActorSystemName);
            ColoredConsole.WriteCreationEvent($"CREATED '{Constants.ActorSystemName}' ActorSystem.");

            // Create Props for UserActor and then create it
            IActorRef userActorRef = _movieStreamingActorSystem.ActorOf(UserActor.Props(), Constants.ActorNameUserActor);

            // Send PlayMovieMessage to PlaybackActor
            SendPlayMovieMessage(userActorRef, new PlayMovieMessage("Blood Diamond", 42));
            SendPlayMovieMessage(userActorRef, new PlayMovieMessage("The Departed", 51));

            // Send StopMovieMessage to UserActor
            SendStopMovieMessage(userActorRef, new StopMovieMessage(42));
            SendStopMovieMessage(userActorRef, new StopMovieMessage(51));

            // Send PlayMovieMessage to PlaybackActor
            SendPlayMovieMessage(userActorRef, new PlayMovieMessage("Usual Suspect", 42));


            Console.WriteLine("  Press ENTER key to terminate ActorSystem...");
            Console.ReadLine();

            // Action-002: Tell ActorSystem (and all child actors) to temrinate 
            ColoredConsole.WriteCreationEvent($"TERMINATING '{Constants.ActorSystemName}' ActorSystem.");
            _movieStreamingActorSystem.Terminate();

            // Action-003: Wait for ActorSystem to finish shutting down
            Task whenTerminatedTask =_movieStreamingActorSystem.WhenTerminated;
            whenTerminatedTask.Wait();

            ColoredConsole.WriteUserPrompt("  Press ENTER key to close the application...");
            ColoredConsole.WriteUserPrompt("");
        }

        private static void SendPlayMovieMessage(IActorRef actorRef, PlayMovieMessage pmm)
        {
            ColoredConsole.LogSendMessage("PlayMovieMessage", pmm.ToString(), actorRef);
            actorRef.Tell(pmm);
        }

        private static void SendStopMovieMessage(IActorRef actorRef, StopMovieMessage smm)
        {
            ColoredConsole.LogSendMessage("StopMovieMessage", smm.ToString(), actorRef);
            actorRef.Tell(smm);
        }
    }
}

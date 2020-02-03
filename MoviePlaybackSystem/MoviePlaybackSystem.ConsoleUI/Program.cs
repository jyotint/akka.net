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
            ColoredConsole.WriteTitle("DemoAkkaNet02: Running Akka.NET demo...");

            // Create an ActionSystem
            _movieStreamingActorSystem = ActorSystem.Create(Constants.ActorSystemName);
            ColoredConsole.WriteCreationEvent($"CREATED '{Constants.ActorSystemName}' ActorSystem.");

            Props playbackActorProps = Props.Create<PlaybackActor>();

            // Create PlaybackActor using Props
            IActorRef playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, Constants.ActorNamePlaybackActor);

            // Send PlayMovieMessage to PlaybackActor
            ColoredConsole.WriteSentMessage("Sending (Telling) PlayMovieMessage messages...");
            playbackActorRef.Tell(new PlayMovieMessage("Blood Diamond", 42));
            playbackActorRef.Tell(new PlayMovieMessage("The Departed", 51));

            // Action - 004: Send PoisonPill message
            ColoredConsole.WriteSentMessage("Sending (Telling) PoisonPill message...");
            playbackActorRef.Tell(PoisonPill.Instance);

            // Action - 005: Send PlayMovieMessage to PlaybackActor
            ColoredConsole.WriteSentMessage("Sending (Telling) PlayMovieMessage messages...");
            playbackActorRef.Tell(new PlayMovieMessage("Usual Suspect", 42));



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
    }
}

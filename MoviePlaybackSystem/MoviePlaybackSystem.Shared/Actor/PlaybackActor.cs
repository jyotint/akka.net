using Akka.Actor;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    // Action-003 - Create an untyped actor
    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            ColoredConsole.WriteCreationEvent($"CREATED '{Constants.ActorNamePlaybackActor}' Actor.");
        }

        protected override void OnReceive(object message)
        {
            ColoredConsole.WriteReceivedMessage($"  Received Message on '{Constants.ActorNamePlaybackActor}' Actor...");
            if (message is string)
            {
                ColoredConsole.WriteReceivedMessage($"    Move Title: '{message}'.");
            }
            else if(message is int)
            {
                ColoredConsole.WriteReceivedMessage($"    User Id: '{message.ToString()}'.");
            }
            else if(message is PlayMovieMessage)
            {
                var pmm = message as PlayMovieMessage;
                ColoredConsole.WriteReceivedMessage($"    Move Title: '{pmm.MovieTitle}', User Id: {pmm.UserId}.");
            }
            else
            {
                ColoredConsole.WriteReceivedMessage($"    ERROR: Unknown '{message.GetType().ToString()}' type received!");
                Unhandled(message);
            }
        }
    }
}

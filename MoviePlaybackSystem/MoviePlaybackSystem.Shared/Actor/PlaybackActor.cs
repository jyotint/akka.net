using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    // Action-003 - Create an untyped actor
    public class PlaybackActor : CustomUntypedActor
    {
        public PlaybackActor()
        {
            ColoredConsole.WriteCreationEvent($"CREATED '{ActorName}' Actor.");
        }

        public static Akka.Actor.Props Props()
        {
            return Akka.Actor.Props.Create<PlaybackActor>(() => new PlaybackActor());
        }

        protected override void OnReceive(object message)
        {
            ColoredConsole.WriteReceivedMessage($"  Received Message on '{Constants.ActorNamePlaybackActor}' Actor...");

            if(message is PlayMovieMessage pmm)
            {
                ColoredConsole.WriteReceivedMessage($"    Move Title: '{pmm.MovieTitle}', User Id: {pmm.UserId}.");
            }
            // else if()
            // {
            // }
            else
            {
                ColoredConsole.WriteReceivedMessage($"    ERROR: Unknown '{message.GetType().ToString()}' type received!");
                Unhandled(message);
            }
        }
    }
}

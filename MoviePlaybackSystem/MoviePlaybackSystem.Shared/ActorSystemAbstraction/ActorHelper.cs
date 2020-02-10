using Akka.Actor;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.ActorSystemAbstraction
{
    public class ActorHelper
    {
        public IActorRef ActorRef { get; private set; }

        public ActorHelper(IActorRef actorRef)
        {
            ActorRef = actorRef;
        }

        public void SendMessageAsynchronous(object message)
        {
            ColoredConsole.LogSendAsynchronousMessage(message.GetType().Name, message.ToString(), ActorRef);
            ActorRef.Tell(message);
        }
    }
}

using System.Collections.Generic;
using Akka.Actor;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    public class UserCoordinatorActor : CustomUntypedActor
    {
        private ActorSystemHelper _actorSystemHelper;

        public UserCoordinatorActor(ActorSystemHelper actorSystemHelper)
            : base()
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");
            _actorSystemHelper = actorSystemHelper;
        }

        public static Akka.Actor.IActorRef Create(ActorSystemHelper actorSystemHelper)
        {
            return Context.ActorOf(UserCoordinatorActor.Props(actorSystemHelper), ActorPaths.UserCoordinatorActor.Name);
        }
        public static Akka.Actor.Props Props(ActorSystemHelper actorSystemHelper)
        {
            return Akka.Actor.Props.Create(() => new UserCoordinatorActor(actorSystemHelper));
        }

        override protected void OnReceive(object message)
        {
            IActorRef actorRef;

            switch(message)
            {
                case PlayMovieMessage pmm:
                    actorRef = CreateOrGetChildActor(pmm.UserId);
                    _actorSystemHelper.SendAsynchronousMessage(actorRef, message);
                    break;
                case StopMovieMessage smm:
                    actorRef = CreateOrGetChildActor(smm.UserId);
                    _actorSystemHelper.SendAsynchronousMessage(actorRef, message);
                    break;
                default:
                    ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] OnReceive(): ERROR: Unknown '{message.GetType().ToString()}' type received!");
                    Unhandled(message);
                    break;
            }
        }

        private IActorRef CreateOrGetChildActor(int userId)
        {
            IActorRef actorRef;

            var childActorMetaData = ActorPaths.GetUserActorMetaData(userId.ToString());
            // ColoredConsole.WriteTemporaryDebugMessage($"User Actor Path: '{userActorMetaData.Path}'");

            // Use ResolveOne or Identity message to get the Actor Reference
            // actorRef = _actorSystemHelper.GetActorRefUsingIdentity(userActorMetaData.Path);
            actorRef = _actorSystemHelper.GetActorRefUsingResolveOne(childActorMetaData.Path);
            if(actorRef == null)
            {
                actorRef = _actorSystemHelper.CreateActor(Context, UserActor.Props(userId), childActorMetaData.Name);
                ColoredConsole.WriteCreationEvent($"    [{this.ActorName}] '{this.ActorName}' has created new child '{childActorMetaData.Name}' actor for UserId {userId}.");
            }

            return actorRef;
        }
    }
}

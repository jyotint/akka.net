using Akka.Actor;
using MoviePlaybackSystem.Shared.Utils;
using System;

namespace MoviePlaybackSystem.Shared.ActorSystemAbstraction
{
    public abstract class CustomUntypedActor : UntypedActor
    {
        public CustomUntypedActor()
        {
            var actorName = Self.Path.Name;

            // ColoredConsole.WriteCreationEvent($"  [{actorName}] CREATING '{actorName}' actor with address '{Self.Path.ToString()}'.");
            ColoredConsole.WriteCreationEvent($"  [{actorName}] Path: '{Self.Path.ToString()}', UID: '{Self.Path.Uid}', Address: '{Self.Path.Address}', Parent: '{Self.Path.Parent}', Root: '{Self.Path.Root}'.");
        }

        #region Public Read-only properties

        public string ActorName
        { 
            get 
            { 
                return Self.Path.Name;
            } 
        }

        public string ActorAddress
        { 
            get 
            { 
                return Self.Path.ToString();
            }
        }

        #endregion // Actor Lifecyle Hooks

        #region Public Methods

        public void SendMessageAsynchronous(object message)
        {
            ColoredConsole.LogSendAsynchronousMessage(message.GetType().Name, message.ToString(), Self);
            Self.Tell(message);
        }

        #endregion // Public Methods


        #region Actor Lifecyle Hooks

        protected override void PreStart()
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{ActorName}] Lifecycle event: PreStart event hook...");
            base.PreStart();
        }

        protected override void PostStop()
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{ActorName}] Lifecycle event: PostStop event hook...");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{ActorName}] Lifecycle event: PreRestart event hook (Reason: '{reason.Message}')...");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{ActorName}] Lifecycle event: PostRestart event hook (Reason: '{reason.Message}')...");
            base.PostRestart(reason);
        }

        #endregion // Actor Lifecyle Hooks
    }
}

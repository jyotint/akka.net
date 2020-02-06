using Akka.Actor;
using MoviePlaybackSystem.Shared.Utils;
using System;

namespace MoviePlaybackSystem.Shared.Actor
{
    public abstract class CustomUntypedActor : UntypedActor
    {
        public CustomUntypedActor()
        {
            ColoredConsole.WriteCreationEvent($"CREATING '{Self.Path.Name}' actor with address '{Self.Path.ToString()}'.");
            //ColoredConsole.WriteCreationEvent($"  Name: '{Self.Path.Name}', '{Self.Path.Uid}', '{Self.Path.Address}', '{Self.Path.Parent}', '{Self.Path.Root}'.");
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

        #region Actor Lifecyle Hooks

        protected override void PreStart()
        {
            ColoredConsole.WriteLifeCycleEvent($"  Lifecycle event for '{ActorName}' actor: PreStart event hook...");
            base.PreStart();
        }

        protected override void PostStop()
        {
            ColoredConsole.WriteLifeCycleEvent($"  Lifecycle event for '{ActorName}' actor: PostStop event hook...");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColoredConsole.WriteLifeCycleEvent($"  Lifecycle event for '{ActorName}' actor: PreRestart event hook (Reason: '{reason.Message}')...");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColoredConsole.WriteLifeCycleEvent($"  Lifecycle event for '{ActorName}' actor:  PostRestart event hook (Reason: '{reason.Message}')...");
            base.PostRestart(reason);
        }

        #endregion // Actor Lifecyle Hooks
    }
}

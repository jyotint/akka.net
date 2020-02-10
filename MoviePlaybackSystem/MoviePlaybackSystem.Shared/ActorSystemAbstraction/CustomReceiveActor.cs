using Akka.Actor;
using MoviePlaybackSystem.Shared.Utils;
using System;

namespace MoviePlaybackSystem.Shared.ActorSystemAbstraction
{
    public class CustomReceiveActor : ReceiveActor
    {
        protected readonly string _actorName;

        public CustomReceiveActor()
        {
            _actorName = Self.Path.Name;

            ColoredConsole.WriteCreationEvent($"CREATED '{_actorName}' actor with address '{Self.Path.ToString()}'.");
            //ColoredConsole.WriteCreationEvent($"  Name: '{Self.Path.Name}', '{Self.Path.Uid}', '{Self.Path.Address}', '{Self.Path.Parent}', '{Self.Path.Root}'.");
        }


        #region Actor Lifecyle Hooks

        protected override void PreStart()
        {
            ColoredConsole.WriteLifeCycleEvent($"PreStart event hook for '{_actorName}' actor...");
            base.PreStart();
        }

        protected override void PostStop()
        {
            ColoredConsole.WriteLifeCycleEvent($"PostStop event hook for '{_actorName}' actor...");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColoredConsole.WriteLifeCycleEvent($"PreRestart event hook for '{_actorName}' actor (Reason: '{reason.Message}')...");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColoredConsole.WriteLifeCycleEvent($"PostRestart event hook for '{_actorName}' actor (Reason: '{reason.Message}')...");
            base.PostRestart(reason);
        }

        #endregion // Actor Lifecyle Hooks
    }
}

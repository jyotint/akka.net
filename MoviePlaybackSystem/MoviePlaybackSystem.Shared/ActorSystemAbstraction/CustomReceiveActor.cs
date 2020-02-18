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

            // ColoredConsole.WriteCreationEvent($"  [{_actorName}] CREATED '{_actorName}' actor with address '{Self.Path.ToString()}'.");
            ColoredConsole.WriteCreationEvent($"  [{_actorName}] Name: '{Self.Path.Name}', '{Self.Path.Uid}', '{Self.Path.Address}', '{Self.Path.Parent}', '{Self.Path.Root}'.");
        }


        #region Actor Lifecyle Hooks

        protected override void PreStart()
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{_actorName}] PreStart event hook...");
            base.PreStart();
        }

        protected override void PostStop()
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{_actorName}] PostStop event hook...");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{_actorName}] PreRestart event hook (Reason: '{reason.Message}')...");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColoredConsole.WriteLifeCycleEvent($"  [{_actorName}] PostRestart event hook (Reason: '{reason.Message}')...");
            base.PostRestart(reason);
        }

        #endregion // Actor Lifecyle Hooks
    }
}

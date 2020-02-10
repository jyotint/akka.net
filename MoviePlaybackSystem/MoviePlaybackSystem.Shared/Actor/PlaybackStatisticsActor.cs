using Akka.Actor;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.CustomException;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    public class PlaybackStatisticsActor : CustomUntypedActor
    {
        private ActorSystemHelper _actorSystemHelper;
        private IActorRef _moviePlayCounterActor;

        public PlaybackStatisticsActor(ActorSystemHelper actorSystemHelper)
            : base()
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");
            _actorSystemHelper = actorSystemHelper;

            _moviePlayCounterActor = _actorSystemHelper.CreateActor(Context, MoviePlayCounterActor.Props(), MoviePlayCounterActor.GetActorName());
        }

        public static string GetActorName()
        {
            return Constants.ActorNamePlaybackStatisticsActor;
        }

        public static Akka.Actor.IActorRef Create(ActorSystemHelper actorSystemHelper)
        {
            return Context.ActorOf(PlaybackStatisticsActor.Props(actorSystemHelper), GetActorName());
        }

        public static Akka.Actor.Props Props(ActorSystemHelper actorSystemHelper)
        {
            return Akka.Actor.Props.Create<PlaybackStatisticsActor>(() => new PlaybackStatisticsActor(actorSystemHelper));
        }

        override protected void OnReceive(object message)
        {
            if(message is IncrementMoviePlayCountMessage msg)
            {
                _actorSystemHelper.SendAsynchronousMessage(_moviePlayCounterActor, msg);
            }
            else
            {
                ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] OnReceive(): ERROR: Unknown '{message.GetType().ToString()}' type received!");
                Unhandled(message);
            }
        }        

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                {
                    if(exception is SimulatedCorruptStateException)
                    {
                        return Directive.Restart;
                    }
                    if(exception is SimulatedTerribleMovieException)
                    {
                        return Directive.Resume;
                    }
                    else
                    {
                        return Directive.Restart;
                    }
                }
            );

            // return base.SupervisorStrategy();
        }
    }
}

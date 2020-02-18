using Akka.Actor;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;
using MoviePlaybackSystem.Shared.CustomException;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.Actor
{
    public class PlaybackStatisticsActor : CustomUntypedActor
    {
        private IActorRef _moviePlayCounterActor;

        public PlaybackStatisticsActor()
            : base()
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");

            _moviePlayCounterActor = Context.ActorOf(MoviePlayCounterActor.Props(), ActorPaths.MoviePlayCounterActor.Name);
        }

        public static Akka.Actor.IActorRef Create()
        {
            return Context.ActorOf(PlaybackStatisticsActor.Props(), ActorPaths.PlaybackStatisticsActor.Name);
        }

        public static Akka.Actor.Props Props()
        {
            return Akka.Actor.Props.Create<PlaybackStatisticsActor>(() => new PlaybackStatisticsActor());
        }

        override protected void OnReceive(object message)
        {
            if(message is IncrementMoviePlayCountMessage msg)
            {
                _moviePlayCounterActor.Tell(message);
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

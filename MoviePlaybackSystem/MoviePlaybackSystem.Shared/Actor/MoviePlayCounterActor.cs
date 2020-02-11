using System.Collections.Generic;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.CustomException;
using MoviePlaybackSystem.Shared.Utils;
using MoviePlaybackSystem.Shared.ActorSystemAbstraction;

namespace MoviePlaybackSystem.Shared.Actor
{
    public class MoviePlayCounterActor : CustomUntypedActor
    {
        private readonly Dictionary<string, int> _moviePlayCount;

        public MoviePlayCounterActor()
            : base()
        {
            ColoredConsole.WriteCreationEvent($"  [{this.ActorName}] '{ActorName}' actor constructor.");
            _moviePlayCount = new Dictionary<string, int>();
        }

        public static Akka.Actor.IActorRef Create()
        {
            return Context.ActorOf(MoviePlayCounterActor.Props(), ActorPaths.MoviePlayCounterActor.Name);
        }

        public static Akka.Actor.Props Props()
        {
            return Akka.Actor.Props.Create<MoviePlayCounterActor>(() => new MoviePlayCounterActor());
        }

        override protected void OnReceive(object message)
        {
            if(message is IncrementMoviePlayCountMessage msg)
            {
                ProcessIncrementMoviePlayCountMessage(msg);
            }
            else
            {
                ColoredConsole.WriteReceivedMessage($"    [{this.ActorName}] OnReceive(): ERROR: Unknown '{message.GetType().ToString()}' type received!");
                Unhandled(message);
            }
        }

        private void ProcessIncrementMoviePlayCountMessage(IncrementMoviePlayCountMessage message)
        {
            var newCount = 0;
            if(_moviePlayCount.ContainsKey(message.MovieTitle))
            {
                newCount = _moviePlayCount[message.MovieTitle] + message.Count;
                _moviePlayCount[message.MovieTitle] = newCount;
            }
            else
            {
                newCount = message.Count;
                _moviePlayCount.Add(message.MovieTitle, newCount);
            }

            // Simulated Exception - SimulatedCorruptStateException
            if(newCount > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            // Simulated Exception - SimulatedCorruptStateException
            if(message.MovieTitle == "Terminator")
            {
                throw new SimulatedTerribleMovieException();
            }

            ColoredConsole.WriteStateChangeEvent($"      [{this.ActorName}] State: Movie '{message.MovieTitle}' has been watched {newCount} times.");
        }
    }
}

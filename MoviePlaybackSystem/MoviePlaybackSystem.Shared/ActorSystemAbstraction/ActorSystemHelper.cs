using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.Shared.ActorSystemAbstraction
{
    public class ActorSystemHelper
    {
        public ActorSystem AkkaActorSystem { get; private set; }

        public void CreateActorSystem(string actorSystemName)
        {
            // Read Akka configuration and create an ActionSystem
            Config akkaConfig = HoconConfiguration.ReadAndParse(Constants.AkkaConfigurationFileName);

            AkkaActorSystem = ActorSystem.Create(actorSystemName, akkaConfig);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorSystemName}' ActorSystem.");
        }

        public void TerminateActorSystem()
        {
            // Notify ActorSystem (and all child actors) to temrinate 
            ColoredConsole.WriteCreationEvent($"TERMINATING '{AkkaActorSystem.Name}' ActorSystem.");
            AkkaActorSystem.Terminate();

            // Wait for ActorSystem to finish shutting down
            Task whenTerminatedTask = AkkaActorSystem.WhenTerminated;
            whenTerminatedTask.Wait();
        }

        public IActorRef CreateActor(Props props, string actorName)
        {
            IActorRef actorRef = AkkaActorSystem.ActorOf(props, actorName);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorName}' actor.");

            return actorRef;
        }

        public IActorRef CreateActor(IUntypedActorContext context, Props props, string actorName)
        {
            IActorRef actorRef = context.ActorOf(props, actorName);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorName}' actor.");

            return actorRef;
        }

        public ActorHelper CreateActorHelper(Props props, string actorName)
        {
            IActorRef actorRef = AkkaActorSystem.ActorOf(props, actorName);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorName}' actor.");

            return new ActorHelper(actorRef);
        }

        public void SendAsynchronousMessage(IActorRef actorRef, object message)
        {
            ColoredConsole.LogSendAsynchronousMessage(message.GetType().Name, message.ToString(), actorRef);
            actorRef.Tell(message);
        }

        // public T SendSynchronousMessage<T>(IActorRef actorRef, object message, TimeSpan timeout)
        // {
        //     ColoredConsole.LogSendSynchronousMessage(message.GetType().Name, message.ToString(), actorRef);
        //     return actorRef.Ask<T>(message, timeout).Result;
        // }

        public T SendSynchronousMessage<T>(ActorSelection actorSelection, object message, TimeSpan timeout)
        {
            ColoredConsole.LogSendSynchronousMessage(message.GetType().Name, message.ToString(), actorSelection);
            return actorSelection.Ask<T>(message, timeout).Result;
        }
        public IActorRef GetActorRefUsingIdentity(string actorPath)
        {
            IActorRef actorRef = null;

            try
            {
                var randomId = (new Random()).Next(1, 10000);
                var selection = AkkaActorSystem.ActorSelection(actorPath);
                var identity = SendSynchronousMessage<ActorIdentity>(selection, new Identify(randomId), TimeSpan.FromSeconds(30));
                if(System.Convert.ToInt32(identity.MessageId) == randomId)
                {
                    return identity.Subject;
                }
            }
            catch (Exception)
            {
                actorRef = null;
            }

            return actorRef;
        }

        public IActorRef GetActorRefUsingResolveOne(string actorPath)
        {
            IActorRef actorRef = null;

            try
            {
                var selection = AkkaActorSystem.ActorSelection(actorPath);
                actorRef = selection.ResolveOne(new TimeSpan(0, 0, 30)).Result;
            }
            catch (ActorNotFoundException)
            {
                actorRef = null;
                // ColoredConsole.WriteTemporaryDebugMessage($"  ERROR: Actor not found ('{actorPath}')!");
                // ColoredConsole.WriteError(ex.Message);
            }
            catch (Exception)
            {
                actorRef = null;
                // ColoredConsole.WriteError(ex.Message);
            }

            return actorRef;
        }
    }
}

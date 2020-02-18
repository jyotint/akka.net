using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using MoviePlaybackSystem.Shared.Utils;
using Petabridge.Cmd.Host;

namespace MoviePlaybackSystem.Shared.ActorSystemAbstraction
{
    public static class ActorSystemHelper
    {
        public static ActorSystem AkkaActorSystem { get; private set; }

        public static void CreateActorSystem(string actorSystemName)
        {
            // Read Akka configuration and create an ActionSystem
            Config config = HoconConfiguration.ReadAndParse(Constants.AkkaConfigurationFileName);
            ColoredConsole.WriteTitle($"Application Name: {config.GetString("application.info.name")}");

            if (AkkaActorSystem == null)
            {
                AkkaActorSystem = ActorSystem.Create(actorSystemName, config);
                ColoredConsole.WriteCreationEvent($"CREATED '{actorSystemName}' ActorSystem.");
            }

            var petabridgeCmdHost = PetabridgeCmd.Get(GetAkkaActorSystem());
            petabridgeCmdHost.RegisterCommandPalette(Petabridge.Cmd.Remote.RemoteCommands.Instance);
            petabridgeCmdHost.Start();
        }

        public static void TerminateActorSystem()
        {
            // Notify ActorSystem (and all child actors) to temrinate 
            ColoredConsole.WriteCreationEvent($"TERMINATING '{GetAkkaActorSystem().Name}' ActorSystem.");
            AkkaActorSystem.Terminate();

            // Wait for ActorSystem to finish shutting down
            Task whenTerminatedTask = AkkaActorSystem.WhenTerminated;
            whenTerminatedTask.Wait();

            AkkaActorSystem = null;
        }

        public static ActorSystem GetAkkaActorSystem()
        {
            return ActorSystemHelper.AkkaActorSystem;
        }

        public static IActorRef CreateActor(Props props, string actorName)
        {
            IActorRef actorRef = GetAkkaActorSystem().ActorOf(props, actorName);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorName}' actor.");

            return actorRef;
        }

        public static IActorRef CreateActor(IUntypedActorContext context, Props props, string actorName)
        {
            IActorRef actorRef = context.ActorOf(props, actorName);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorName}' actor.");

            return actorRef;
        }

        public static IActorRef CreateActorHelper(Props props, string actorName)
        {
            IActorRef actorRef = GetAkkaActorSystem().ActorOf(props, actorName);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorName}' actor.");

            return actorRef;
        }

        public static IActorRef CreateActorHelper(IUntypedActorContext context, Props props, string actorName)
        {
            IActorRef actorRef = context.ActorOf(props, actorName);
            ColoredConsole.WriteCreationEvent($"CREATED '{actorName}' actor.");

            return actorRef;
        }

        public static void SendAsynchronousMessage(IActorRef actorRef, object message)
        {
            if(actorRef == null)
                throw new ArgumentNullException("ActorRef", "Actor Reference not set or is null!");

            ColoredConsole.LogSendAsynchronousMessage(message.GetType().Name, message.ToString(), actorRef);
            actorRef.Tell(message);
        }

        // public static T SendSynchronousMessage<T>(IActorRef actorRef, object message, TimeSpan timeout)
        // {
        //     ColoredConsole.LogSendSynchronousMessage(message.GetType().Name, message.ToString(), actorRef);
        //     return actorRef.Ask<T>(message, timeout).Result;
        // }

        public static T SendSynchronousMessage<T>(ActorSelection actorSelection, object message, TimeSpan timeout)
        {
            ColoredConsole.LogSendSynchronousMessage(message.GetType().Name, message.ToString(), actorSelection);
            return actorSelection.Ask<T>(message, timeout).Result;
        }
        public static IActorRef GetActorRefUsingIdentity(string actorPath)
        {
            IActorRef actorRef = null;

            try
            {
                var randomId = (new Random()).Next(1, 10000);
                var selection = GetAkkaActorSystem().ActorSelection(actorPath);
                var identity = SendSynchronousMessage<ActorIdentity>(selection, new Identify(randomId), TimeSpan.FromSeconds(30));
                if (System.Convert.ToInt32(identity.MessageId) == randomId)
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

        public static IActorRef GetActorRefUsingResolveOne(string actorPath)
        {
            IActorRef actorRef = null;

            try
            {
                var selection = GetAkkaActorSystem().ActorSelection(actorPath);
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

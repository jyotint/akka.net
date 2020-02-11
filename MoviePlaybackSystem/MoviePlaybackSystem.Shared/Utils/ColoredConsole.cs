using System;
using Akka.Actor;

namespace MoviePlaybackSystem.Shared.Utils
{
    public static class ColoredConsole
    {
        public static void LogSendAsynchronousMessage(string messageType)
        {
            ColoredConsole.WriteSentMessage($"  Sending[Async] '{messageType}' message...");
        }
        public static void LogSendAsynchronousMessage(string messageType, string messageData)
        {
            ColoredConsole.WriteSentMessage($"  Sending[Async] '{messageType}' [{messageData}] message...");
        }
        public static void LogSendAsynchronousMessage(string messageType, IActorRef actorRef)
        {
            ColoredConsole.WriteSentMessage($"  Sending[Async] '{messageType}' message to {GetActorInformation(actorRef)}...");
        }
        public static void LogSendAsynchronousMessage(string messageType, string messageData, IActorRef actorRef)
        {
            ColoredConsole.WriteSentMessage($"  Sending[Async] '{messageType}' [{messageData}] message to {GetActorInformation(actorRef)}...");
        }
        public static void LogSendSynchronousMessage(string messageType, ActorSelection actorSelection)
        {
            ColoredConsole.WriteSentMessage($"  Sending[Sync]  '{messageType}' message to {GetActorSelectionInformation(actorSelection)}...");
        }
        public static void LogSendSynchronousMessage(string messageType, string messageData, ActorSelection actorSelection)
        {
            ColoredConsole.WriteSentMessage($"  Sending[Sync]  '{messageType}' [{messageData}] message to {GetActorSelectionInformation(actorSelection)}...");
        }

        public static string GetActorInformation(IActorRef actorRef)
        {
            return (actorRef == null) ? "" : $"'{actorRef.Path.Name}' ({actorRef.Path.ToStringWithAddress()})";
        }
        public static string GetActorSelectionInformation(ActorSelection actorSelection)
        {
            return (actorSelection == null) ? "" : $"'{actorSelection.PathString}'";
        }

        public static void WriteTitle(string message) => WriteLineInColor(message, ConsoleColor.DarkYellow);
        public static void WriteError(string message) => WriteLineInColor(message, ConsoleColor.Red);
        public static void WriteCreationEvent(string message) => WriteLineInColor(message, ConsoleColor.Magenta);
        public static void WriteLifeCycleEvent(string message) => WriteLineInColor(message, ConsoleColor.DarkMagenta);
        public static void WriteSentMessage(string message) => WriteLineInColor(message, ConsoleColor.Cyan);
        public static void WriteReceivedMessage(string message) => WriteLineInColor(message, ConsoleColor.DarkCyan);
        public static void WriteUserInstructions(string message) => WriteLineInColor(message, ConsoleColor.DarkYellow);
        public static void WriteUserPrompt(string message) => WriteInColor(message, ConsoleColor.DarkYellow);
        public static void WriteStateChangeEvent(string message) => WriteLineInColor(message, ConsoleColor.Green);

        public static void WriteBehaviorChangeEvent(string message) => WriteLineInColor(message, ConsoleColor.DarkGreen);
        public static void WritePersistenceBehaviorEvent(string message) => WriteLineInColor(message, ConsoleColor.DarkGreen);
        public static void WriteTemporaryDebugMessage(string message) => WriteLineInColor(message, ConsoleColor.Blue);

        public static void WriteLineInColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            var beforeForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeForegroundColor;
        }

        public static void WriteInColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            var beforeForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write($"{message} ");

            Console.ForegroundColor = beforeForegroundColor;
        }
    }
}

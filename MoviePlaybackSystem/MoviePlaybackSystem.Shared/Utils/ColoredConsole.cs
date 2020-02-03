using System;

namespace MoviePlaybackSystem.Shared.Utils
{
    public static class ColoredConsole
    {
        public static void WriteTitle(string message) => WriteLineInColor(message, ConsoleColor.DarkYellow);
        public static void WriteError(string message) => WriteLineInColor(message, ConsoleColor.Red);
        public static void WriteCreationEvent(string message) => WriteLineInColor(message, ConsoleColor.Cyan);
        public static void WriteLifeCycleEvent(string message) => WriteLineInColor(message, ConsoleColor.DarkCyan);
        public static void WriteSentMessage(string message) => WriteLineInColor(message, ConsoleColor.Blue);
        public static void WriteReceivedMessage(string message) => WriteLineInColor(message, ConsoleColor.Blue);
        public static void WriteUserInstructions(string message) => WriteLineInColor(message, ConsoleColor.DarkYellow);
        public static void WriteUserPrompt(string message) => WriteLineInColor(message, ConsoleColor.DarkYellow);
        public static void WriteStateChangeEvent(string message) => WriteLineInColor(message, ConsoleColor.Green);

        public static void WriteBehaviorChangeEvent(string message) => WriteLineInColor(message, ConsoleColor.DarkGreen);
        public static void WritePersistenceBehaviorEvent(string message) => WriteLineInColor(message, ConsoleColor.DarkGreen);
        public static void WriteTemporaryDebugMessage(string message) => WriteLineInColor(message, ConsoleColor.Magenta);

        public static void WriteLineInColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            var beforeForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeForegroundColor;
        }
    }
}
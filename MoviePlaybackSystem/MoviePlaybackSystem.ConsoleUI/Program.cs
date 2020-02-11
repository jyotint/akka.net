using System;
using CommandLine;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.ConsoleUI
{
    class Program
    {
        private static readonly string CommandSeparator = " ";
        private static MoviePlaybackSystemHelper _moviePlaybackSystemHelper;

        static void Main(string[] args)
        {
            try
            {
                ColoredConsole.WriteTitle("MoviePlaybackSystem: Running Akka.NET demo...");
                ColoredConsole.WriteTemporaryDebugMessage($"Environment.UserInteractive: {Environment.UserInteractive}");

                _moviePlaybackSystemHelper = new MoviePlaybackSystemHelper();

                // Start ActorSystem
                _moviePlaybackSystemHelper.StartActorSystem();
                // Run interactive session with user inputting commands
                RunUserInteractiveSession();
                // Terminate ActorSystem
                _moviePlaybackSystemHelper.TerminateActorSystem();

                ColoredConsole.WriteTitle("MoviePlaybackSystem: Quitting Akka.NET demo.");
            }
            catch (Exception ex)
            {
                ColoredConsole.WriteError($"MoviePlaybackSystem: Exception occurred (type: '{ex.GetType().Name}')");
                ColoredConsole.WriteError(ex.Message);
                ColoredConsole.WriteError(ex.StackTrace);
            }
            finally
            {
                // Terminate ActorSystem
                _moviePlaybackSystemHelper.TerminateActorSystem();
                _moviePlaybackSystemHelper = null;
            }
        }

        private static void RunUserInteractiveSession()
        {
            bool quit = false;

            // Create Command Line parser
            ColoredConsole.WriteUserInstructions("HELP: 'help' to list commands. 'help <command>' to get a help for specific command.");
            // var parser = new CommandLine.Parser(with => with.HelpWriter = null);
            var parser = CommandLine.Parser.Default;

            do
            {
                try
                {
                    PauseFor();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ColoredConsole.WriteUserPrompt("Enter a command: ");
                    var commandLineArray = Console.ReadLine().Split(CommandSeparator);

                    parser.ParseArguments<CommandParser.StartMovieOptions, CommandParser.StopMovieOptions, CommandParser.QuitOptions>(commandLineArray)
                        .MapResult(
                            (CommandParser.StartMovieOptions options) =>
                            {
                                return _moviePlaybackSystemHelper.StartPlayingMovie(options);
                            },
                            (CommandParser.StopMovieOptions options) =>
                            {
                                return _moviePlaybackSystemHelper.StopPlayingMovie(options);
                            },
                            (CommandParser.QuitOptions options) =>
                            {
                                quit = true;
                                return 0;
                            },
                            errs =>
                            {
                                return 1;
                            }
                        );
                }
                catch (Exception ex)
                {
                    ColoredConsole.WriteError($"Exception occurred (type: '{ex.GetType().Name}')");
                    ColoredConsole.WriteError(ex.Message);
                    ColoredConsole.WriteError(ex.StackTrace);
                }
            } while (quit == false);
        }

        private static void PauseFor(int pauseInMilliSeconds = 1000)
        {
            System.Threading.Thread.Sleep(pauseInMilliSeconds);
        }
    }
}

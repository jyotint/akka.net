using System;
using CommandLine;
using MoviePlaybackSystem.Shared.Actor;
using MoviePlaybackSystem.Shared.Helpers;
using MoviePlaybackSystem.Shared.Message;
using MoviePlaybackSystem.Shared.Utils;

namespace MoviePlaybackSystem.ConsoleUI
{
    class Program
    {
        private static readonly string CommandSeparator = " ";

        static void Main(string[] args)
        {
            try
            {
                ColoredConsole.WriteTitle("MoviePlaybackSystem: Running Akka.NET demo...");

                // Start ActorSystem
                MoviePlaybackSystemHelper.StartActorSystem(true);
                ActorPaths.LogAllActorPaths();

                // Run interactive session with user inputting commands
                RunUserInteractiveSession();
                
                // Terminate ActorSystem
                // MoviePlaybackSystemHelper.TerminateActorSystem();

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
                MoviePlaybackSystemHelper.TerminateActorSystem();
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
                                return MoviePlaybackSystemHelper.StartPlayingMovie(new PlayMovieMessage(options.MovieTitle, options.UserId));
                            },
                            (CommandParser.StopMovieOptions options) =>
                            {
                                return MoviePlaybackSystemHelper.StopPlayingMovie(new StopMovieMessage(options.UserId));
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

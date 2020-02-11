using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace MoviePlaybackSystem.ConsoleUI
{
    public class CommandParser
    {
        [Verb("start", HelpText = "Start a movie for a user.")]
        public class StartMovieOptions
        {
            [Value(0, MetaName = "UserId", HelpText = "Specify UserId of user for whom movie will start")]
            public int UserId { get; set; }

            [Value(1, MetaName = "MovieTitle", Min = 1, HelpText = "Specify MovieTitle for movie to start")]
            public IEnumerable<string> MovieTitleList { get; set; }

            public string MovieTitle
            {
                get
                {
                    return string.Join(" ", MovieTitleList);
                }
            }
        }

        [Verb("stop", HelpText = "Stop a movie for a user.")]
        public class StopMovieOptions
        {
            [Value(0, MetaName = "UserId", HelpText = "Specify UserId of user for whom movie will stop")]
            public int UserId { get; set; }
        }

        [Verb("quit", HelpText = "Quit this application.")]
        public class QuitOptions
        {
        }

        public static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            HelpText helpText = null;
            
            if (errs.IsVersion())  //check if error is version request
                helpText = HelpText.AutoBuild(result);
            else
            {
                helpText = HelpText.AutoBuild(result, h =>
                {
                    h.AdditionalNewLineAfterOption = false;
                    return HelpText.DefaultParsingErrorsHandler(result, h);
                }, e => e);
            }

            Console.WriteLine(helpText);
        }
    }
}

using System;
using Akka.Actor;
using MoviePlaybackSystem.Shared.Message;

namespace MoviePlaybackSystem.Shared.Actor
{
    // Action-009: Create a typed actor
    public class NewPlaybackActor : CustomReceiveActor
    {
        public NewPlaybackActor()
        {
            Console.WriteLine($"CREATED '{Constants.ActorNameNewPlaybackActor}' Actor.");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            // Action-014: Comment previous line and only handle message from a specific User (User Id)
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserId == Constants.HandleMessageFromUserId);
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Console.WriteLine($"  Received Message on '{Constants.ActorNameNewPlaybackActor}' Actor...");
            Console.WriteLine($"    Move Title: '{message.MovieTitle}', User Id: {message.UserId}.");
        }
    }
}

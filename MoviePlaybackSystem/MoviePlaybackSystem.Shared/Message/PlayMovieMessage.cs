using System;
using System.Collections.Generic;
using System.Text;

namespace MoviePlaybackSystem.Shared.Message
{
    // Action-008: Create PlayMovieMessage class
    public class PlayMovieMessage
    {
        public string MovieTitle { get; private set; }

        public int UserId { get; private set; }

        public PlayMovieMessage(string movietitle, int userId)
        {
            MovieTitle = movietitle;
            UserId = userId;
        }
    }
}

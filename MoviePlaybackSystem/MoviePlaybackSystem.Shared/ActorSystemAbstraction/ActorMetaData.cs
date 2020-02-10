namespace MoviePlaybackSystem.Shared.ActorSystemAbstraction
{
    /// <summary>
    /// Meta-data class. Nested/child actors can build path 
    /// based on their parent(s) / position in hierarchy.
    /// </summary>
    public class ActorMetaData
    {
        public ActorMetaData(string name, ActorMetaData parent = null, string suffix = null)
        {
            Name = name;
            Parent = parent;
            // if no parent, we assume a top-level actor
            var parentPath = parent != null ? parent.Path : "/user";
            Path = string.Format("{0}/{1}", parentPath, Name);
            if(suffix != null)
            {
                Path = Path + suffix;
            }
        }

        public string Name { get; private set; }

        public ActorMetaData Parent { get; set; }

        public string Path { get; private set; }
    }
}

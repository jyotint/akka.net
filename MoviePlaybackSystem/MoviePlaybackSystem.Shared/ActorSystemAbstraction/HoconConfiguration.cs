using Akka.Configuration;
using System.IO;

namespace MoviePlaybackSystem.Shared.ActorSystemAbstraction
{
    public class HoconConfiguration
    {
        public static Config ReadAndParse(string hoconConfigFileName)
        {
            Config akkaConfig = null;

            if(File.Exists(hoconConfigFileName))
            {
                var configData = File.ReadAllText(hoconConfigFileName);
                akkaConfig = ConfigurationFactory.ParseString(configData);
            }
            else
            {
                throw new FileNotFoundException($"'{hoconConfigFileName}' configuration file is missing!", hoconConfigFileName);
            }

            return akkaConfig;
        }
    }
}

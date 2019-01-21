using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace checkTwitch
{
    class CommandLineOptions
    {
        [Option("clientId", Required = true, HelpText = "twitchClientId")]
        public string twitchClientId { get; set; }

        [Option("urlOrUserName", Required = true, HelpText = "please twitch url or username")]
        public string urlOrUserName { get; set; }

        [Usage(ApplicationAlias = "checkTwitch")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                    new Example("checks if the user is broadcasting on Twitch",new CommandLineOptions{twitchClientId="<yours Twitch client id>",urlOrUserName = "<url or username>"})
                };
            }
        }
    }
}

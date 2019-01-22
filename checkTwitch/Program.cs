using System;
using CommandLine;

namespace checkTwitch
{
    class Program
    {
        static int Main(string[] args)
        {
            bool result = false;
            var status = string.Empty;
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(opt =>
                {
                    var twitchInformation = new TwitchInformation(opt.twitchClientId);
                    result = twitchInformation.IsLiveStreaming(opt.urlOrUserName, out status);
                })
                .WithNotParsed(error =>
                {
                    Console.WriteLine("\ncommand line parse error.");
                    result = false;
                });

            if (result)
            {
                Console.WriteLine(DateTime.Now.ToString() + "\tOnline");
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString() + "\tOffline");
            }
            Console.WriteLine("status:" + status);

            return result == true ? 0 : 1;
        }
    }
}

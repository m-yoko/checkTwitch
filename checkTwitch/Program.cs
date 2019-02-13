using System;
using McMaster.Extensions.CommandLineUtils;

namespace checkTwitch
{
    [Subcommand(typeof(PublishTokenModeOptions))]
    [Subcommand(typeof(CheckOptions))]
    class Program
    {
        static int Main(string[] args)
        {
            var status = string.Empty;

            var result = CommandLineApplication.Execute<Program>(args);
            switch (result)
            {
                case 0:
                    Console.WriteLine($"{DateTime.Now}    Online");
                    break;
                case 1:
                    Console.WriteLine($"{DateTime.Now}    Offline");
                    break;
                default:
                    break;
            }
            return result;
        }
        private int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 1;
        }
    }
}

using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;

namespace checkTwitch
{


    [Command("Check")]
    [HelpOption]
    public class CheckOptions
    {

        [Option(Template = "--userNameUrl|--uu", Description = "Target UserName or URL")]
        public string UserNameOrUrl { get; set; }

        [Required]
        [Option(Template = "--tokenString|--ts", Description = "Your Token")]
        public string TokenString { get; set; }

        [Required]
        [Option(Template = "--tokenType|--tt", Description = "Your Token type")]
        public string TokenType { get; set; }



        public int OnExecute()
        {
            var token = new Token(TokenString, TokenType);
            var twitchInfo = new TwitchInformation(token);
            var result = twitchInfo.IsLiveStreaming(UserNameOrUrl, out var status);
            Console.WriteLine(status);
            return result==true?0:1;
        }

    }


    //[Command(FullName = "PublishToken",Name = "pt")]
    [Command(FullName = "PublishToken", Name = "pt")]
    [HelpOption]
    public class PublishTokenModeOptions
    {
        [Required]
        [Option(Template = "--clientId|--ci", Description = "Your Twitch ClientID")]
        public string ClientId { get; set; }

        [Required]
        [Option(Template = "--clientSecret|--cs", Description = "Your Twitch ClientSecret")]
        public string ClientSecret { get; set; }

        public int OnExecute()
        {
            var twitchInfo = new TwitchInformation(ClientId, ClientSecret);
            var token = twitchInfo.PublishToken();
            Console.WriteLine($"Token:{token.TokenString}");
            Console.WriteLine($"TokenType:{token.TokenType}");
            Console.WriteLine($"ExpireDate:{token.ExpireDate}");
            return 2;
        }
    }
}

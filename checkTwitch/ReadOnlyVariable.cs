using System;

namespace checkTwitch
{
    public class Constant
    {
        public static readonly Uri ApiNewBaseUrl = new Uri("https://api.twitch.tv/helix/");
        public static readonly Uri ApiPublishTokenUrl = new Uri("https://id.twitch.tv/oauth2/token");
        public static readonly Uri ApiValidateTokenUrl = new Uri("https://id.twitch.tv/oauth2/validate/");
        public static readonly Uri ApiCheckHostingUrl = new Uri("https://tmi.twitch.tv/hosts");
    }
}
using RestSharp.Deserializers;
using System.Collections.Generic;

namespace checkTwitch
{
    public class StreamInformation
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }

        [DeserializeAs(Name = "user_id")]
        public string UserId { get; set; }

        [DeserializeAs(Name = "user_name")]
        public string UserName { get; set; }

        [DeserializeAs(Name = "game_id")]
        public string GameId { get; set; }

        [DeserializeAs(Name = "community_ids")]
        public List<string> CommunityIds { get; set; }

        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "title")]
        public string Title { get; set; }

        [DeserializeAs(Name = "viewer_count")]
        public string ViewerCount { get; set; }

        [DeserializeAs(Name = "started_at")]
        public string StartedAt { get; set; }

        [DeserializeAs(Name = "language")]
        public string Language { get; set; }

        [DeserializeAs(Name = "thumbnail_url")]
        public string ThumbnailUrl { get; set; }

    }

    public class ChannelInformation
    {
        public string Mature { get; set; }
        public string Status { get; set; }

        [DeserializeAs(Name = "broadcaster_language")]
        public string BroadcasterLanguage { get; set; }

        [DeserializeAs(Name = "display_name")] public string DisplayName { get; set; }
        public string Game { get; set; }
        public string Language { get; set; }
        [DeserializeAs(Name = "_id")] public string Id { get; set; }
        public string Name { get; set; }
        [DeserializeAs(Name = "created_at")] public string CreatedAt { get; set; }
        [DeserializeAs(Name = "updated_at")] public string UpdatedAt { get; set; }
        public string Partner { get; set; }
        public string Logo { get; set; }
        [DeserializeAs(Name = "video_banner")] public string VideoBanner { get; set; }

        [DeserializeAs(Name = "profile_banner")]
        public string ProfileBanner { get; set; }

        [DeserializeAs(Name = "profile_banner_background_color")]
        public string ProfileBannerBackgroundColor { get; set; }

        public string Url { get; set; }
        public string Views { get; set; }
        public string Followers { get; set; }
    }

    public class ResponseStreamInformation
    {
        [DeserializeAs(Name = "data")]
        public List<StreamInformation> StreamInformation { get; set; }
    }

    public class UserInformation
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }

        [DeserializeAs(Name = "login")]
        public string LoginName { get; set; }

        [DeserializeAs(Name = "display_name")]
        public string DisplayName { get; set; }

        [DeserializeAs(Name = "description")]
        public string Description { get; set; }

        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [DeserializeAs(Name = "offline_image_url")]
        public string OfflineImageUrl { get; set; }

        [DeserializeAs(Name = "view_count")]
        public string ViewCount { get; set; }

    }

    public class ResponseUserInformation
    {
        [DeserializeAs(Name = "data")]
        public List<UserInformation> UserInformations { get; set; }
    }

    public class HostingInformation
    {
        [DeserializeAs(Name = "host_id")] public string HostId { get; set; }
        [DeserializeAs(Name = "target_id")] public string TargetId { get; set; }

        [DeserializeAs(Name = "host_login")] public string HostUserName { get; set; }

        [DeserializeAs(Name = "target_login")] public string TargetUserName { get; set; }

        [DeserializeAs(Name = "host_display_name")]
        public string HostDisplayName { get; set; }

        [DeserializeAs(Name = "target_display_name")]
        public string TargetDisplayName { get; set; }
    }

    public class ResponseHostingInformation
    {
        [DeserializeAs(Name = "hosts")] public List<HostingInformation> Hosts { get; set; }
    }

    public class ResponseTokenInformation
    {
        [DeserializeAs(Name = "access_token")] public string AccessToken { get; set; }

        [DeserializeAs(Name = "expires_in")] public long RemainingSeconds { get; set; }

        [DeserializeAs(Name = "scope")] public List<string> ScopeStrings { get; set; }

        [DeserializeAs(Name = "token_type")] public string TokenType { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}

using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace checkTwitch
{
    class TwitchInformation
    {
        public class StreamInformation
        {
            [DeserializeAs(Name = "_id")]
            public string id { get; set; }
            public string game { get; set; }
            public string viewers { get; set; }
            public string video_height { get; set; }
            public string average_fps { get; set; }
            public string delay { get; set; }
            public string created_at { get; set; }
            public string is_playlist { get; set; }

            [DeserializeAs(Name = "channel")]
            public ChannelInformation channelInformation { get; set; }
        }

        public class ChannelInformation
        {
            public string mature { get; set; }
            public string status { get; set; }
            public string broadcaster_language { get; set; }
            public string display_name { get; set; }
            public string game { get; set; }
            public string language { get; set; }
            [DeserializeAs(Name = "_id")]

            public string id { get; set; }
            public string name { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string partner { get; set; }
            public string logo { get; set; }
            public string video_banner { get; set; }
            public string profile_banner { get; set; }
            public string profile_banner_background_color { get; set; }
            public string url { get; set; }
            public string views { get; set; }
            public string followers { get; set; }
        }
        public class ResponseStreamInformation
        {
            [DeserializeAs(Name = "stream")]
            public StreamInformation streamInformation { get; set; }

        }
        public class UserInformation
        {
            [DeserializeAs(Name = "_id")]
            public string id { get; set; }
            public string bio { get; set; }
            public string created_at { get; set; }
            public string display_name { get; set; }
            public string logo { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string updated_at { get; set; }
        }
        public class ResponseUserInformation
        {

            [DeserializeAs(Name = "_total")]

            public int userCount { get; set; }
            [DeserializeAs(Name = "users")]

            public List<UserInformation> UserInformations { get; set; }
        }

        public class HostingInformation
        {
            [DeserializeAs(Name = "host_id")]
            public string hostId { get; set; }
            [DeserializeAs(Name = "target_id")]
            public string targetId { get; set; }

            [DeserializeAs(Name = "host_login")]
            public string hostUserName { get; set; }

            [DeserializeAs(Name = "target_login")]
            public string targetUserName { get; set; }

            [DeserializeAs(Name = "host_display_name")]
            public string hostDisplayName { get; set; }

            [DeserializeAs(Name = "target_display_name")]
            public string targetDisplayName { get; set; }
        }

        public class ResponseHostingInformation
        {
            [DeserializeAs(Name = "hosts")]
            public List<HostingInformation> hosts { get; set; }

        }

        private string TwitchClientId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="twitchClientId"></param>
        public TwitchInformation(in string twitchClientId)
        {
            this.TwitchClientId = twitchClientId;
        }

        /// <summary>
        /// get GET response.
        /// </summary>
        /// <param name="uriString">please set uri</param>
        /// <param name="twitchClientId">please set clientId</param>
        /// <returns></returns>
        private IRestResponse getResponse(in string uriString)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(uriString);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("Accept", "application/vnd.twitchtv.v5+json");
            request.AddHeader("Client-ID", this.TwitchClientId);
            return client.Execute(request);
        }

        /// <summary>
        /// get GET response.
        /// </summary>
        /// <param name="uriString">please set uri</param>
        /// <param name="twitchClientId">please set clientId</param>
        /// <returns></returns>
        private IRestResponse GetGETResponse(in string baseUriString, in List<Parameter> parameters)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUriString);
            var request = new RestRequest(Method.GET);
            foreach (var param in parameters)
            {
                request.AddParameter(param);
            }
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("Client-ID", this.TwitchClientId);
            return client.Execute(request);
        }




        public bool IsOtherHosting(in string userId)
        {
            var parameters = new List<Parameter>();
            parameters.Add(new Parameter("include_logins", "1", ParameterType.GetOrPost));
            parameters.Add(new Parameter("host", userId, ParameterType.GetOrPost));
            var hostingResponse = GetGETResponse("https://tmi.twitch.tv/hosts", parameters);
            var deserializer = new JsonDeserializer();
            var hostingInformation = deserializer.Deserialize<ResponseHostingInformation>(hostingResponse);
            var result = false;
            if (hostingInformation.hosts.FirstOrDefault()?.targetId == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }


        /// <summary>
        /// Check if it is a valid client ID.
        /// </summary>
        /// <returns>valid:true</returns>
        public bool IsCorrectClientId()
        {
            bool result = false;
            const string userName = "twitchjp";

            const string apiBaseUrl = "https://api.twitch.tv/kraken/";

            string translateUrl = apiBaseUrl + "users?login=" + userName;

            var translateResponse = getResponse(translateUrl);

            var deserializer = new JsonDeserializer();
            ResponseUserInformation responseUserInformation = deserializer.Deserialize<ResponseUserInformation>(translateResponse);
            if (responseUserInformation.userCount == 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Check if it is on air.
        /// </summary>
        /// <param name="urlOrUserName">
        /// url         ex:https://www.twitch.tv/monstercat
        /// userName    ex:monstercat
        /// </param>
        /// <param name="status">live status for output</param>
        /// <returns></returns>
        public bool IsLiveStreaming(in string urlOrUserName, out string status)
        {
            if (!this.IsCorrectClientId())
            {
                Console.WriteLine("Twitch client id error.");
                Console.WriteLine("Please check the client id.");
                status = "";
                return false;
            }
            var isUrl = urlOrUserName.StartsWith("http");
            var userName = string.Empty;
            if (isUrl)
            {
                try
                {
                    var urlObj = new Uri(urlOrUserName);
                    userName = urlObj.Segments[1].Replace("/", "");
                }
                catch (Exception e)
                {
                    Console.WriteLine("!urlError!");
                    Console.WriteLine("Reason:");
                    Console.WriteLine(e);
                    Environment.Exit(1);
                }
            }
            else
            {
                userName = urlOrUserName;
            }
            const string apiBaseUrl = "https://api.twitch.tv/kraken/";

            string translateUrl = apiBaseUrl + "users?login=" + userName;

            var translateResponse = getResponse(translateUrl);

            var deserializer = new JsonDeserializer();
            ResponseUserInformation responseUserInformation = deserializer.Deserialize<ResponseUserInformation>(translateResponse);
            if (responseUserInformation.userCount == 0)
            {
                Console.WriteLine("user information is not found");
                Environment.Exit(1);
            }
            var streamUrl = apiBaseUrl + "streams/" + responseUserInformation.UserInformations.FirstOrDefault().id;
            var streamResponse = getResponse(streamUrl);
            ResponseStreamInformation responseStreamInformation =
                deserializer.Deserialize<ResponseStreamInformation>(streamResponse);
            bool result;
            if (responseStreamInformation.streamInformation == null)
            {
                result = false;
                status = "";
                if (IsOtherHosting(responseUserInformation.UserInformations.FirstOrDefault().id))
                {
                    status = "hosting now";
                }

            }
            else
            {
                result = true;
                status = responseStreamInformation.streamInformation.channelInformation.status;
            }

            return result;
        }
    }

}

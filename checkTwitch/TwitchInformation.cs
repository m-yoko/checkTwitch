using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace checkTwitch
{



    class TwitchInformation
    {

        #region variable

        public string TwitchClientId { get; }

        public Token Token { get; set; }

        public string ClientSecret { get; }

        #endregion




        #region mainClass

        /// <summary>
        /// 
        /// </summary>
        /// <param name="twitchClientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="checkClientId"></param>
        public TwitchInformation(in string twitchClientId, in string clientSecret, in bool checkClientId = true)
        {
            this.TwitchClientId = twitchClientId;
            if (checkClientId)
            {
                if (!this.IsValidClientId())
                {
                    Console.WriteLine("Twitch client id error.");
                    Console.WriteLine("Please check the client id.");
                    Environment.Exit(1);
                }
            }
            this.ClientSecret = clientSecret;
        }

        public TwitchInformation(in Token token)
        {
            this.Token = token;
        }

        public Token PublishToken(string scopes = null)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("client_id", this.TwitchClientId, ParameterType.GetOrPost),
                new Parameter("client_secret", this.ClientSecret, ParameterType.GetOrPost),
                new Parameter("grant_type", "client_credentials", ParameterType.GetOrPost)
            };
            if (scopes != null)
            {
                parameters.Add(new Parameter("scope", scopes, ParameterType.GetOrPost));
            }

            var tokenResponse = RestSharpWrapper.GetResponse(Constant.ApiPublishTokenUrl, parameters,Method.POST);
            var deserializer = new JsonDeserializer();
            var tokenInformation = deserializer.Deserialize<ResponseTokenInformation>(tokenResponse);
            Token token = null;
            if (tokenInformation.AccessToken != null)
            {
                token = new Token(tokenInformation.AccessToken, tokenInformation.TokenType,
                    tokenInformation.RemainingSeconds);
            }
            else
            {
                Console.WriteLine("!PublishTokenError!");
                Console.WriteLine("Reason:");
                Console.WriteLine(tokenInformation.Status);
                Console.WriteLine(tokenInformation.Message);
                Environment.Exit(1);
            }

            return token;
        }

        public bool IsOtherHosting(in string userId,out string status)
        {
            var parameters = new List<Parameter>
            {
                new Parameter("include_logins", "1", ParameterType.GetOrPost),
                new Parameter("host", userId, ParameterType.GetOrPost)
            };
            var hostingResponse = RestSharpWrapper.GetResponse(Constant.ApiCheckHostingUrl, parameters);
            var deserializer = new JsonDeserializer();
            var hostingInformation = deserializer.Deserialize<ResponseHostingInformation>(hostingResponse);
            var result = false;
            if (hostingInformation.Hosts.FirstOrDefault()?.TargetId == null)
            {
                result = false;
                status = "";
            }
            else
            {
                result = true;
                status =$"HostName:{hostingInformation.Hosts.FirstOrDefault().TargetDisplayName}\n" +
                        $"HostId:{hostingInformation.Hosts.FirstOrDefault().TargetId}\n";
            }

            return result;
        }


        /// <summary>
        /// Check if it is a valid client ID.
        /// </summary>
        /// <returns>valid:true</returns>
        public bool IsValidClientId()
        {
            var result = false;
            Uri.TryCreate(Constant.ApiNewBaseUrl, "streams",out var checkUrl);
            var parameters = new List<Parameter>
            {
                new Parameter("Client-ID", this.TwitchClientId, ParameterType.HttpHeader),
                new Parameter("first", "1", ParameterType.GetOrPost)
            };
            var response = RestSharpWrapper.GetResponse(checkUrl, parameters);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result = true;
                    break;
                case HttpStatusCode.Unauthorized:
                    result = false;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Check if it is on air.
        /// </summary>
        /// <param name="urlOrUserName">
        /// url         ex: https://www.twitch.tv/monstercat
        /// userName    ex: monstercat
        /// </param>
        /// <param name="status">live status for output</param>
        /// <returns></returns>
        public bool IsLiveStreaming(in string urlOrUserName, out string status)
        {
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

            Uri.TryCreate(Constant.ApiNewBaseUrl, "streams", out var streamUrl);
            var streamUrlParams = new List<Parameter>()
            {
                new Parameter("Authorization",$"{this.Token.TokenType} {this.Token.TokenString}",ParameterType.HttpHeader),
                new Parameter("user_login", userName, ParameterType.GetOrPost)                
            };
            
            var streamResponse = RestSharpWrapper.GetResponse(streamUrl, streamUrlParams);
            var deserializer = new JsonDeserializer();
            var streamInformation =
                deserializer.Deserialize<ResponseStreamInformation>(streamResponse);
            var result = false;
            if (!streamInformation.StreamInformation.Any())
            {
                result = false;
                status = "";
                Uri.TryCreate(Constant.ApiNewBaseUrl, "users", out var userInfoUrl);
                var userInfoUrlParams = new List<Parameter>()
                {
                    new Parameter("Authorization",$"{this.Token.TokenType} {this.Token.TokenString}",ParameterType.HttpHeader),
                    new Parameter("login", userName, ParameterType.GetOrPost)
                };
                var userInformationResponse = RestSharpWrapper
                    .GetResponse(userInfoUrl, userInfoUrlParams);
                var UserInformation = deserializer.Deserialize<ResponseUserInformation>(userInformationResponse);
                if (UserInformation.UserInformations.FirstOrDefault()?.Id !=null)
                {
                    if (IsOtherHosting(UserInformation.UserInformations.FirstOrDefault().Id,out status))
                    {
                        status += "user is hosting now";
                        result = true;
                    }
                }
            }
            else
            {
                result = true;
                status = streamInformation.StreamInformation.FirstOrDefault().Title;
            }

            return result;
        }

        #endregion
    }
}
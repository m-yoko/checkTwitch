using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace checkTwitch
{
    public class Token
    {
        public string TokenString { get; }
        public string TokenType { get; }
        public DateTime ExpireDate { get; }

        public Token(in string tokenString, in string tokenType, in DateTime expireDate)
            : this(tokenString, tokenType)
        {
            this.ExpireDate = expireDate;
        }

        public Token(in string tokenString, in string tokenType, in long remainingSeconds)
            : this(tokenString, tokenType)
        {
            this.ExpireDate = DateTime.Now.AddSeconds(remainingSeconds);
        }

        public Token(in string tokenString, in string tokenType)
        {
            this.TokenString = tokenString;
            this.TokenType = tokenType;
        }

        public bool isValidToken()
        {
            var result = false;
            var parameters = new List<Parameter>
            {
                new Parameter("Authorization", this.TokenString, ParameterType.HttpHeader),
            };
            var response = RestSharpWrapper.GetResponse(Constant.ApiValidateTokenUrl, parameters);

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
    }
}
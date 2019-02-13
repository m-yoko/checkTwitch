using System;
using System.Collections.Generic;
using RestSharp;

namespace checkTwitch
{
    public class RestSharpWrapper
    {
        /// <summary>
        ///     get GET response.
        /// </summary>
        /// <param name="uri">please set uri</param>
        /// <param name="parameters">please set uri parameters.</param>
        /// <returns></returns>
        public static IRestResponse GetResponse(in Uri uri, in IEnumerable<Parameter> parameters,in Method methodType = Method.GET)
        {
            var client = new RestClient
            {
                BaseUrl = uri
            };
            var request = new RestRequest(methodType);
            foreach (var param in parameters) request.AddParameter(param);

            request.RequestFormat = DataFormat.Json;
            return client.Execute(request);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Top10Covid19Cases.Helpers
{
    public static class ApiCallerHelper
    {
        public async static Task<string> makeGetRequest(string baseURL, string endpoint, Dictionary<string, string> headers, Dictionary<string, string> parameters)
        {
            string body;

            //BUILD FULL URL WITH PARAMETERS
            string urlString = baseURL + "/" + endpoint;

            if (parameters.Count > 0) { urlString += "?"; }

            foreach (var parameter in parameters)
            {
                urlString += parameter.Key + "=" + parameter.Value + "&";
            }

            //remove remaining &
            urlString.Remove(urlString.Length - 1);

            //BUILD HEADERS OBJECT


            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlString),
                Headers =
                {
                    { "x-rapidapi-key", "7857be9d59mshd8e5240b7613bddp192a42jsn56baa348938c" },
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                }
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

            }

                
            return body;
        }
    }
}

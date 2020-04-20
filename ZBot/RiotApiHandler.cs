using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZBot
{
    public class RiotApiHandler
    {
        private readonly IHttpClientFactory _clientFactory;

        public RiotApiHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private HttpRequestMessage CreateRequestWithHeaders(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };
            
            request.Headers.Add("X-Riot-Token", ConfigurationManager.AppSettings["RiotAPIKey"]);

            return request;
        }

        public async Task<T> ApiRequest<T>(string url)
        {
            var request = CreateRequestWithHeaders(url);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            string apiResponseString = await response.Content.ReadAsStringAsync();
           
            return JsonConvert.DeserializeObject<T>(apiResponseString);
        }
    }
}
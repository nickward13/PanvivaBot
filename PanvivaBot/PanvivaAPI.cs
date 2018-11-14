using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PanvivaBot
{
    public class PanvivaAPI
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly string PanvivaAPIEndpoint = Environment.GetEnvironmentVariable("PanvivaAPIEndpoint");
        private static readonly string OcpApimSubscriptionKey = Environment.GetEnvironmentVariable("Ocp-Apim-Subscription-Key");

        public static async Task<List<PanvivaNLSSearchResponseItem>> NaturalLanguageSearchAsync(string searchTerm)
        {
            try
            {
                searchTerm = System.Net.WebUtility.UrlEncode(searchTerm);
                Client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);
                var response = await Client.GetStringAsync($"https://{PanvivaAPIEndpoint}/operations/Artefacts/nls?query={searchTerm}");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<PanvivaNLSSearchResponseItem>>(response);
            }
            catch (Exception e)
            {
                return new List<PanvivaNLSSearchResponseItem>();
            }
        }
    }
}

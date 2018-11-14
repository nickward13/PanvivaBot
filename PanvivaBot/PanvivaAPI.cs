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

        public static async Task<string> NaturalLanguageSearchAsync(string searchTerm)
        {
            searchTerm = System.Net.WebUtility.UrlEncode(searchTerm);
            var response = await Client.GetStringAsync($"https://{PanvivaAPIEndpoint}/api/NaturalLanguageSearch?term={searchTerm}");
            return response;
        }
    }
}

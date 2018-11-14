using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PanvivaBot
{
    public class PanvivaAPI
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> NaturalLanguageSearchAsync(string searchTerm)
        {
            searchTerm = System.Net.WebUtility.UrlEncode(searchTerm);
            var response = await client.GetStringAsync($"https://panvivaapifunctionapp20181114114338.azurewebsites.net/api/NaturalLanguageSearch?term={searchTerm}");
            return response;
        }
    }
}

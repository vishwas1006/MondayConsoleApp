using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MondayConsoleApp.Services
{
    public class MondayClient
    {
        private readonly HttpClient _client;

        public MondayClient(string apiToken)
        {
            _client = new HttpClient();

            _client.DefaultRequestHeaders.Add("Authorization", apiToken);

        }

        public async Task<string> SendQueryAsync(string query)
        {
            var requestBody = new
            {
                query = query
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("https://api.monday.com/v2", content);

            return await response.Content.ReadAsStringAsync();
        }


    }
}

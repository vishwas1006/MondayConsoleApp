using System.Text;
using System.Text.Json;
using DotNetEnv;

Env.Load();

string? apiToken = Environment.GetEnvironmentVariable("MONDAY_API_TOKEN");

using HttpClient client = new();

client.DefaultRequestHeaders.Add("Authorization", apiToken);

string query = @"
{
    boards{
        id
        name
    }
}";

var requestBody = new
{
    query = query
};

string jsonBody = JsonSerializer.Serialize(requestBody);

var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

HttpResponseMessage response = await client.PostAsync("https://api.monday.com/v2", content);

string result = await response.Content.ReadAsStringAsync();

//Console.WriteLine(result);

using JsonDocument document = JsonDocument.Parse(result);

var boards = document.RootElement.
                GetProperty("data").GetProperty("boards");

foreach(var board in boards.EnumerateArray())
{
    string id = board.GetProperty("id").GetString();
    string name = board.GetProperty("name").GetString();

    Console.WriteLine($"Board ID: {id}");
    Console.WriteLine($"Bpard Name:{name}");
    Console.WriteLine("------------------------");
}
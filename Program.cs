using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to OpenAI Console App!");
        Console.WriteLine("Enter your input:");
        var input = Console.ReadLine();

        var client = new HttpClient();
        var queryString = new Dictionary<string, string>()
        {
            { "prompt", input },
        };

        var apiKey = "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";
        var engine_name = "text-davinci-002";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var jsonContent = new StringContent(JsonConvert.SerializeObject(queryString), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"https://api.openai.com/v1/engines/{engine_name}/completions", jsonContent);
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine(json);
        if (response.IsSuccessStatusCode)
        {
            dynamic data = JsonConvert.DeserializeObject(json);
            if (data != null && data.choices != null && data.choices.Count > 0)
            {
                var output = data.choices[0].text;
                Console.WriteLine("Output:");
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("No output available");
            }
        }
        else
        {
            Console.WriteLine("Error: " + response.ReasonPhrase);
        }
    }
}
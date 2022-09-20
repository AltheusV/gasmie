using Newtonsoft.Json;
using RestSharp;

namespace gasmie.src.notion
{
    public static class NotionRequest
    {
        public async static ValueTask<List<ScraperDto>> GetScraperDtos(string url, string id, string key)
        {
            var client = new RestClient(url);
            var request = new RestRequest($"databases/{id}/query", Method.Post);
            request.AddHeader("Notion-Version", "2022-06-28");
            request.AddHeader("Authorization", $"Bearer {key}");

            var response = await client.ExecuteAsync(request);
            return GetScraperDtos(response);
        }

        private static List<ScraperDto> GetScraperDtos(RestResponse response)
        {
            var list = new List<ScraperDto>();

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Notion Response returned error: {response.ErrorMessage}");
                return list;
            }

            var content = response.Content;
            if (content is null)
            {
                Console.WriteLine("Notion Response returned with no content.");
                return list;
            }

            var databaseResponse = JsonConvert.DeserializeObject<DatabaseResponse>(content);
            if (databaseResponse == null)
            {
                Console.WriteLine("Notion DatabaseResponse could not be serialized.");
                return list;
            }

            var results = databaseResponse.Results;
            if (results == null)
            {
                Console.WriteLine("Serialized DatabaseResponse returned with no results.");
                return list;
            }

            foreach (var result in results)
            {
                var url = result?.Properties?.Link?.Url ?? "";
                var name = result?.Properties?.Type?.Select?.Name ?? ScraperMode.NONE;

                if (url.Equals("") || name.Equals(ScraperMode.NONE))
                {
                    Console.WriteLine("One of your Notion data on database could not be loaded.");
                    continue;
                }

                list.Add(new ScraperDto(url, name));
            }

            return list;
        }
    }
}
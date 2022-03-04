using System.Net.Http.Headers;
using System.Text.Json;

namespace Lms.Client.Service
{
    public class SWAPI_CRUDService : IIntegrationService
    {
        private static HttpClient httpClient = new HttpClient();
        public SWAPI_CRUDService()
        {
            httpClient.BaseAddress = new Uri("https://swapi.dev/api/");
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Run()
        {
            await GetPepole();
        }

        public async Task GetPepole()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "people");
            
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions();
            option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var people = JsonSerializer.Deserialize<IEnumerable<>>(content);
        }
    }
}

using Lms.Client.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Lms.Client.Service
{
    public class PartialUpdateService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient();
        public PartialUpdateService()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7045");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }
        public async Task Run()
        {
           await PatchResource();
        }
        public async Task PatchResource()
        {
            var patchDoc = new JsonPatchDocument<CourseModifyDto>();
            patchDoc.Replace(c => c.StartDate, DateTime.Now.AddDays(-3));

            var serilizedChangedSet = JsonConvert.SerializeObject(patchDoc);

            var request = new HttpRequestMessage(HttpMethod.Patch,
                "api/courses/7");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serilizedChangedSet);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var updatedCourse = JsonConvert.DeserializeObject<CourseDto>(content);

        }
    }
}

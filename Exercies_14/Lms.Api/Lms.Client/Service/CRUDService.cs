using Lms.Client.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lms.Client.Service
{
    public class CRUDService : IIntegrationService
    {
        private static HttpClient httpClient = new HttpClient();

        public CRUDService()
        {
            httpClient.BaseAddress = new Uri("https://localhost:7045");
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task Run()
        {
            //await GetResource();
            await GetResourceThroughHttpRequestMessage();
            //CreateResource();
            //UpdateResource();
            //DeleteResorce();
        }

        public async Task GetResource()
        {
            var response = await httpClient.GetAsync("api/Courses");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var courses = JsonSerializer.Deserialize<IEnumerable<Course>>(content, options);
            
        }
        public async Task GetResourceThroughHttpRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Courses?withModules=false");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var courses = JsonSerializer.Deserialize<IEnumerable<CoursesWithModulesDto>>(content, options);
        }
        public async Task CreateResource()
        {
            var courseToCreate = new Course()
            {
                Title = "Client Test 2",
                StartDate = DateTime.Now,
            };

            var serializedCourse = JsonSerializer.Serialize(courseToCreate);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Courses");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(serializedCourse);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var createdcourses = JsonSerializer.Deserialize<IEnumerable<Course>>(content, options);
        }
        public async Task UpdateResource()
        {
            var courseToUpdate = new Course()
            {
                Title = "Client Test 3",
                StartDate = DateTime.Now.AddDays(-1)
            };

            var serializedCourse = JsonSerializer.Serialize(courseToUpdate);

            var request = new HttpRequestMessage(HttpMethod.Put,
                "api/Courses/7");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content= new StringContent(serializedCourse);
            request.Content.Headers.ContentType=new MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var createdcourses = JsonSerializer.Deserialize<IEnumerable<Course>>(content, options);
        }
        public async Task DeleteResorce()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                "api/Courses/8");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request); 
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
        }
    }
    
}

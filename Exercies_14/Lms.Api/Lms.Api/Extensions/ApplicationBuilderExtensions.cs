using Bogus;
using Lms.Core.Entities;
using Lms.Data.Data;

namespace Lms.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static Faker faker = new Faker();
        private static LmsApiContext _context = null;
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;
                _context = provider.GetService<LmsApiContext>();

                try
                {
                    await SeedData();
                }
                catch (Exception ex)
                {
                    var log = provider.GetRequiredService<ILogger<Program>>();
                    log.LogError(String.Join(" ", ex.Message));
                }
            }
            return app;
        }
        private static async Task SeedData()
        {
            if (!_context.Course.Any())
            {
                var courses = GetCourses();
                await _context.AddRangeAsync(courses);

                await _context.SaveChangesAsync();
            }
            
            
        }
        private static IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course>();
            for (int i = 0; i < 5; i++)
            {
                var course = new Course
                {
                    //Id = i + 1,
                    Title = faker.Commerce.Categories(1).First(),
                    StartDate = (faker.Date.Past(1)),
                    Modules = GetModules()
                };
                courses.Add(course);
            }
            return courses;
        }
        private static ICollection<Module> GetModules()
        {
            var modules = new List<Module>();
            int randNr = faker.Random.Int(1, 10);
            for (int i = 0; i < randNr; i++)
            {
                var module = new Module
                {
                    Title = faker.Company.CatchPhrase(),
                    StartDate = (faker.Date.Past(1))
                };
                modules.Add(module);
            }
            return modules;
        }
    }
}

using Lms.Client.Service;

namespace Lms.Client.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> RunCrudServiceAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;

                try
                {
                    var logger = provider.GetService<ILogger<Program>>();
                    logger.LogInformation("Host Created.");

                    await provider.GetService<IIntegrationService>().Run();
                }
                catch (Exception generalException)
                {
                    var log = provider.GetService<ILogger<Program>>();
                    log.LogError(generalException, "An exception happened while running the integration service.");
                }
            }
            return app;
        }
    }
}

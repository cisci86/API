using Lms.Client.Extensions;
using Lms.Client.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLogging(configure => configure.AddDebug().AddConsole());
//builder.Services.AddScoped<IIntegrationService, CRUDService>();
//builder.Services.AddScoped<IIntegrationService, PartialUpdateService>();
builder.Services.AddScoped<IIntegrationService, SWAPI_CRUDService>();

var app = builder.Build();

app.RunCrudServiceAsync().GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

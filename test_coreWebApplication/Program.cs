using test_coreWebApplication.DataAccess;
using test_coreWebApplication.DataAccess.Repositories;
using test_coreWebApplication.DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
//builder.Logging.AddFile("Logs/myapp-{Date}.txt"); for serilog if using.
//var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
//ILogger logger = loggerFactory.CreateLogger<Program>();

//logger.LogInformation("Logging information.");
//logger.LogCritical("Logging critical information.");
//logger.LogDebug("Logging debug information.");
//logger.LogError("Logging error information.");
//logger.LogTrace("Logging trace");
//logger.LogWarning("Logging warning.");
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseHttpLogging(); /*to enable httplogging which potentially slow downs the system.*/
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

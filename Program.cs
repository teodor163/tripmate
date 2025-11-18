using Microsoft.AspNetCore.Mvc;
using TripMate_TeodorLazar.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Session storage
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
});

// Razor Pages
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
    });

// Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// STATIC FILES (important for Razor)
app.UseStaticFiles();

// ROUTING (required!)
app.UseRouting();

// Docker Cookie fix
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    Secure = CookieSecurePolicy.None
});

// Authorization (even if unused, must be after routing)
app.UseAuthorization();

// Session (MUST BE after routing + cookie policy)
app.UseSession();

app.MapGet("/stops", () => Results.Redirect("/Stops/Stops"));
app.MapGet("/destinations", () => Results.Redirect("/Destinations/Search"));

// Map Razor Pages (AFTER session)
app.MapRazorPages();

// Root test endpoint
app.MapGet("/", () => Results.Redirect("/Destinations/Search"));



app.Urls.Add("http://0.0.0.0:80");

app.Run();

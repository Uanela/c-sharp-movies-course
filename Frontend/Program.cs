using Frontend.Components;
using Frontend.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); 

var apiUrl = builder.Configuration["ApiUrl"] ?? throw new Exception("ApiUrl is not set.");

builder.Services.AddHttpClient<MoviesClient>(client => client.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<GenresClient>(client => client.BaseAddress = new Uri(apiUrl));

var app = builder.Build(); // Creates an instance of the WebApplication class

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); //redirects requests to HTTPS

app.UseStaticFiles(); // serves static files such as images, CSS, and JavaScript in the wwwwroot folder
app.UseAntiforgery(); // protects the application from cross-site request forgery (CSRF) attacks

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Maps the RazorComponents to the App component

app.Run();

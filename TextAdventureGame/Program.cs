using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using TextAdventureGame;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.MapPost("/command", ([FromBody] GameRequest gameRequest) => { return HandleRequest(gameRequest); });

// http://localhost:5149/ui/index.html
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "ui")),
    RequestPath = "/ui"
});

app.Run();

GameResponse HandleRequest(GameRequest gameRequest)
{
    var response = new GameResponse();

    // game logic here
    response.Text = gameRequest.Input.ToUpper();

    GameState.Index++;
    Console.WriteLine($"Game Index: {GameState.Index}");

    return response;
}
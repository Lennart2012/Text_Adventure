using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using TextAdventureGame;
using LocationStructs;

var builder = WebApplication.CreateBuilder(args);
int GameState = 0;Location CurrentLocation = Locations.OutsideOfCave;
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();
int Lives = 3;
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
    int t;
    int.TryParse(gameRequest.Input, out t);
    CurrentLocation = CurrentLocation.GoTo[t - 1];
    if(CurrentLocation.LivePenalty)
    {
        Lives--;
        if(Lives <= 0)
        {
            response.Text = $"{CurrentLocation.Description} Exhausted, you fall to the ground, and bleed out.";
        }   
    }
    response.Text = CurrentLocation.Description;
    for (int i = 0; i < CurrentLocation.GoTo.Length; i++)
    {
        response.Text += $"{i}. {CurrentLocation.GoTo[i].name} ";
    }
    
    return response;
}
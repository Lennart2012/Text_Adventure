using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using TextAdventureGame;
using LocationStructs;
int d = 0;
var builder = WebApplication.CreateBuilder(args);
int GameState = 0;Location CurrentLocation = Locations.Cave;
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
    if(d == 0)
    {
    Locations.SetUpGoTo();
    d = 1;
    gameRequest.Input = 2.ToString();
    }
    var response = new GameResponse();
    int t;
    int.TryParse(gameRequest.Input, out t);
    CurrentLocation = CurrentLocation.GoTo[t];
    if(CurrentLocation.LivePenalty)
    {
        Lives--;
        if(Lives <= 0)
        {
            response.Text = $"{CurrentLocation.Description} Exhausted, you fall to the ground, and bleed out.";
            return response;
        }  
    }
    response.Text = CurrentLocation.Description;
    for (int i = 0; i < CurrentLocation.GoTo.Length; i++)
    {
        response.Text += $"{i}.{CurrentLocation.GoTo[i].name} ";
    }
    
    return response;
}
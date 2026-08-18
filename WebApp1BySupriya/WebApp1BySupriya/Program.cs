
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        Name = "Supriya Devkota",
        RollNo = "19",
        Message = "Hello ASP.NET Core"
    });
});

app.Run();
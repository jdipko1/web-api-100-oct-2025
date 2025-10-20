var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); //going to eat time to start api
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//Above this line is configuring service and opting in to .net features
var app = builder.Build();

//after this line is configuring the middleware
//actual requests and responses are generated

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); //kestral web  server
}

app.UseAuthorization();

app.MapControllers(); //uses .net reflection to scan app and read those
//routing attributes
//GET requests to vendors
// - create an instance of vendorscontroller
// - call get all vendors async

app.Run();

public partial class Program; //dont make it internal make it public

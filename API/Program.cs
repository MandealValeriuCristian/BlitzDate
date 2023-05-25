using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddIdentityServices(builder.Configuration);
 
var app = builder.Build();
 
// Configure the HTTP request pipeline.
 
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
//app.UseCors(myAngularPolicy);
 
app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
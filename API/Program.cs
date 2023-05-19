using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
 
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
 
builder.Services.AddControllers();
builder.Services.AddCors();
//var myAngularPolicy = "myAngularPolicy";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(myAngularPolicy,
//                          builder =>
//                          {
//                              builder.WithOrigins("http://localhost:4200","https://localhost:4200")
//                                                  .AllowAnyHeader()
//                                                  .AllowAnyMethod();
//                          });
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
 
var app = builder.Build();
 
// Configure the HTTP request pipeline.
 
app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
//app.UseCors(myAngularPolicy);
 
app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
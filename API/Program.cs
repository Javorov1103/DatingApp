using API.Contracts;
using API.Contracts.Services;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IPhotosService, PhotosService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                   };

                   //options.Events = new JwtBearerEvents
                   //{
                   //    OnMessageReceived = context =>
                   //    {
                   //        var accessToken = context.Request.Query["access_token"];

                   //        var path = context.HttpContext.Request.Path;
                   //        if (!string.IsNullOrEmpty(accessToken) &&
                   //            path.StartsWithSegments("/hubs"))
                   //        {
                   //            context.Token = accessToken;
                   //        }

                   //        return Task.CompletedTask;
                   //    }
                   //};
               });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();

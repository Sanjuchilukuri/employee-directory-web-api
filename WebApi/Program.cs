using System.Text;
using Application;
using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Middleware;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
                            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();

        // Add services to the container.
        builder.Services.AddControllers();

        //Added application configuration to builder
        builder.ConfigureApplication();

        //Added Infrastructure configuration to builder
        builder.ConfigureInfrastructure(builder.Configuration);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("V1", new OpenApiInfo
            {
                Title = "EmployeeDirectory Api",
                Version = "V1",
                Description = "A Breif description about API",
                TermsOfService = new Uri("https://sanju.org/privacy-policy"),
                Contact = new OpenApiContact
                {
                    Name = "Support",
                    Email = "support@sanju.org.net",
                    Url = new Uri("https://sanju.org/contact/")
                },
                License = new OpenApiLicense
                {
                    Name = "Use Under XYZ",
                    Url = new Uri("https://sanju.org/about-us/")
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = $"JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            }
                    },
                    new string[]{}
                }
            });

        });

        //CORS
        var apiCorsPolicy = "ApiCorsPolicy";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: apiCorsPolicy,
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                        });
        });

        var app = builder.Build();

        // Apply migrations
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDirectoryDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "My API V1");
            });
        }

        app.UseCors(apiCorsPolicy);

        app.UseMiddleware<GlobalErrorHandling>();

        app.UseMiddleware<RequestLogging>();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

using System.Configuration;
using Infrastructure.DBContext;
using Infrastructure.Interfaces;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IHostApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<IDepartmentsAndRolesRepo, DepartmentsAndRolesRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<ILocationRepo, LocationRepo>();
            builder.Services.AddScoped<IManagerRepo, ManagerRepo>();
            builder.Services.AddScoped<IProjectRepo, ProjectRepo>();
            builder.Services.AddDbContext<EmployeeDirectoryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConeection")));
            // builder.Services.BuildServiceProvider().GetRequiredService<EmployeeDirectoryDbContext>().Database.MigrateAsync();
        }
    }
}
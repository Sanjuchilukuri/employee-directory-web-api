using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IDepartmentsAndRolesServices, DepartmentsAndRolesServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IManagerServices, ManagerServices>();
            builder.Services.AddScoped<IProjectServices, ProjectServices>();
            builder.Services.AddScoped<ILocationServices, LocationServices>();
            builder.Services.AddAutoMapper(typeof(ServiceExtensions));
        }
    }
}
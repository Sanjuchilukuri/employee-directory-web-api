using Infrastructure.DBContext;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class ProjectRepo : IProjectRepo
    {
        private EmployeeDirectoryDbContext _dbContext;

        public ProjectRepo(EmployeeDirectoryDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            var projects = await _dbContext.Projects
                     .Select(project => new Project()
                     {
                         Id = project.Id,
                         Name = project.Name
                     }).ToListAsync();
            return projects!;
        }
    }
}
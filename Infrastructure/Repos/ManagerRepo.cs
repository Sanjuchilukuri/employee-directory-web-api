using Infrastructure.DBContext;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class ManagerRepo : IManagerRepo
    {
        private EmployeeDirectoryDbContext _dbContext;

        public ManagerRepo(EmployeeDirectoryDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Manager>> GetManagersAsync()
        {
            var managers = await _dbContext.Managers
                     .Select(manager => new Manager()
                     {
                         Id = manager.Id,
                         Name = manager.Name
                     }).ToListAsync();
            return managers!;
        }
    }
}
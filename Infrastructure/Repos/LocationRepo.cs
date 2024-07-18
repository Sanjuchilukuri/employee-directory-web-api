using Infrastructure.DBContext;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class LocationRepo : ILocationRepo
    {
        private EmployeeDirectoryDbContext _dbContext;

        public LocationRepo(EmployeeDirectoryDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Location>> GetLocationsAsync()
        {
            var locations = await _dbContext.Locations
                                 .Select(location => new Location()
                                 {
                                     Id = location.Id,
                                     Name = location.Name
                                 }).ToListAsync();
            return locations!;
        }
    }
}
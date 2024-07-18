using Infrastructure.DBContext;
using Infrastructure.Interfaces;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class DepartmentsAndRolesRepo : IDepartmentsAndRolesRepo
    {
        private readonly EmployeeDirectoryDbContext _dbContext;

        public DepartmentsAndRolesRepo(EmployeeDirectoryDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> AddRoleAsync(Role newRole)
        {
            await _dbContext.AddAsync(newRole);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Role>> GetDeparmentRolesAsync(int departmentId)
        {
            var departmentRoles = await _dbContext.Roles
                                     .Where(role => role.DepartmentId == departmentId)
                                     .Select(role => new Role()
                                     {
                                         Id = role.Id,
                                         RoleName = role.RoleName
                                     })
                                     .ToListAsync();
            return departmentRoles!;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var allRoles = await _dbContext.Roles.Select(selcetor => new Role()
            {
                RoleName = selcetor.RoleName,
                Dept = selcetor.Dept,
                Employees = selcetor.Employees
            }).ToListAsync();

            return allRoles;
        }

        public async Task<bool> AddDepartmentAsync(Department dept)
        {
            await _dbContext.Departments.AddAsync(dept);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            var depts = await _dbContext.Departments
                                        .Select(selector => new Department()
                                        {
                                            Id = selector.Id,
                                            DepartmentName = selector.DepartmentName
                                        }).ToListAsync();
            return depts;
        }


    }
}
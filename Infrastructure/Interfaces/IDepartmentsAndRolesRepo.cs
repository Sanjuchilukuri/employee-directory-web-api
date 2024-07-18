
using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IDepartmentsAndRolesRepo 
    {
        public Task<bool> AddRoleAsync(Role role);

        public Task<List<Role>> GetDeparmentRolesAsync(int departmentId);
        
        public Task<bool> AddDepartmentAsync(Department dept);

        public Task<List<Department>> GetDepartmentsAsync();
        public Task<List<Role>> GetAllRoles();
    }
}
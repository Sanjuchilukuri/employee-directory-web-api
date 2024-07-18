
using Application.DTO;

namespace Application.Interfaces
{
    public interface IDepartmentsAndRolesServices 
    {
        public Task<bool> AddRoleAsync(AddRoleDTO addRole);

        public Task<List<RolesDTO>> GetDepartmentRolesAsync(int departmentID);

        public Task<List<DepartmentDTO>> GetDepartmentsAsync();
        
        public Task<bool> AddDepartmentAsync(AddDepartmentDTO addDepartment);
        
        public Task<List<AllRolesDTO>> GetAllRolesAsync();
    }
}

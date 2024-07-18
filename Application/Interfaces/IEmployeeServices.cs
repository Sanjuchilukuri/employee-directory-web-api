using Application.DTO;
using Infrastructure.DTO;

namespace Application.Interfaces
{
    public interface IEmployeeServices
    {
        public Task<bool> AddEmployeeAsync(EmployeeCreateUpdateDTO emp);

        public Task<bool> DeleteEmployeeAsync(string id);

        public Task<List<EmployeesDTO>> GetEmployeesAsync();

        public Task<EmployeeDTO> GetEmployeeAsync(string id);

        public Task<bool> UpdateEmployeeAsync(string id, EmployeeCreateUpdateDTO updatedEmployee);
        
        public Task<List<EmployeesDTO>> GetFilteredEmployeesAsync(Filters filter);
    }
}

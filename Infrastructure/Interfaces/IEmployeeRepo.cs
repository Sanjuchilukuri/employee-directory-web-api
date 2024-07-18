using Infrastructure.DTO;
using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IEmployeeRepo
    {
        public Task<string> GetEmployeeSequenceIDAsync();

        public Task<Employee> GetEmployeeByIdAsync(string Id);

        public Task<List<Employee>> GetAllEmployeesAsync();

        public Task<bool> SaveEmployeeAsync(Employee newEmployee);

        public Task<bool> deleteEmployeeAsync(string id);

        public Task<bool> UpdateEmployeeAsync(Employee employee);

        public Task<List<Employee>> GetFilteredEmployeesAsync(Filters filters);
    }
}
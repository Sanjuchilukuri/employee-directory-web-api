using Infrastructure.DBContext;
using Infrastructure.Interfaces;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DTO;

namespace Infrastructure.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeDirectoryDbContext _dbContext;

        public EmployeeRepo(EmployeeDirectoryDbContext context)
        {
            _dbContext = context;
        }

        public async Task<string> GetEmployeeSequenceIDAsync()
        {
            string? empID = await _dbContext.Employees
                                  .OrderByDescending(emp => emp.EmpId)
                                  .Select(emp => emp.EmpId)
                                  .FirstOrDefaultAsync();
            return empID == null ? "TZ1000" : empID;
        }

        public async Task<bool> SaveEmployeeAsync(Employee newEmployee)
        {
            await _dbContext.Employees.AddAsync(newEmployee);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            var employee = await _dbContext.Employees
                                  .Include(emp => emp.Role)
                                  .ThenInclude(role => role.Dept)
                                  .Where(emp => emp.EmpId == id)
                                  .Select(employee => new Employee()
                                  {
                                      Image = employee.Image,
                                      EmpId = employee.EmpId,
                                      FirstName = employee.FirstName,
                                      LastName = employee.LastName,
                                      DateofBirth = employee.DateofBirth,
                                      PhoneNumber = employee.PhoneNumber,
                                      Email = employee.Email,
                                      JoiningDate = employee.JoiningDate,
                                      Role = employee.Role,
                                      LocationId = employee.LocationId,
                                      ManagerId = employee.ManagerId,
                                      ProjectId = employee.ProjectId
                                  }).SingleOrDefaultAsync();
            return employee!;
        }

        public async Task<bool> deleteEmployeeAsync(string id)
        {
            var result = await _dbContext.Employees
                .Where(emp => emp.EmpId == id)
                .ExecuteDeleteAsync();

            return result > 0;
        }


        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = await _dbContext.Employees
                                    .Include(emp => emp.Role)
                                    .ThenInclude(role => role.Dept)
                                    .Select(employee => new Employee()
                                    {
                                        Image = employee.Image,
                                        FirstName = employee.FirstName,
                                        LastName = employee.LastName,
                                        Email = employee.Email,
                                        EmpId = employee.EmpId,
                                        JoiningDate = employee.JoiningDate,
                                        Role = employee.Role,
                                        Location = employee.Location,
                                        Status = employee.Status
                                    }).ToListAsync();
            return employees;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _dbContext.Update(employee);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Employee>> GetFilteredEmployeesAsync(Filters filters)
        {
            var query = _dbContext.Employees
                            .Include(emp => emp.Role)
                            .ThenInclude(role => role.Dept)
                            .Include(emp => emp.Location)
                             .Select(employee => new Employee
                             {
                                 Image = employee.Image,
                                 FirstName = employee.FirstName,
                                 LastName = employee.LastName,
                                 Email = employee.Email,
                                 EmpId = employee.EmpId,
                                 JoiningDate = employee.JoiningDate,
                                 Role = employee.Role,
                                 Location = employee.Location,
                                 Status = employee.Status
                             })
                            .AsQueryable();

            if (!string.IsNullOrEmpty(filters.alphabet))
            {
                string lowercaseAlphabet = filters.alphabet.ToLower();
                query = query.Where(emp => emp.FirstName.ToLower().StartsWith(lowercaseAlphabet));
            }

            if (!string.IsNullOrEmpty(filters.Department))
            {
                query = query.Where(emp => emp.Role != null && emp.Role.Dept.DepartmentName == filters.Department);
            }

            if (!string.IsNullOrEmpty(filters.status))
            {
                query = query.Where(emp => emp.Status == filters.status);
            }

            if (!string.IsNullOrEmpty(filters.Location))
            {
                query = query.Where(emp => emp.Location != null && emp.Location.Name == filters.Location);
            }

            var employees = await query.ToListAsync();

            return employees;
        }
    }

}
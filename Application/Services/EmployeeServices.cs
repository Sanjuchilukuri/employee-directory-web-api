using Application.Interfaces;
using Infrastructure.Interfaces;
using Application.DTO;
using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.DTO;


namespace Application.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepo _employeeRepo;

        private readonly IMapper _mapper;

        public EmployeeServices(IEmployeeRepo employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddEmployeeAsync(EmployeeCreateUpdateDTO newEmployee)
        {
            string empId = GenerateEmpID();

            if (!String.IsNullOrEmpty(empId))
            {
                Employee emp = _mapper.Map<Employee>(newEmployee, opt =>
                {
                    opt.Items["Empid"] = empId;
                });

                return await _employeeRepo.SaveEmployeeAsync(emp);
            }
            else
            {
                return false;
            }
        }

        private string GenerateEmpID()
        {
            return "TZ" + (Convert.ToInt32(_employeeRepo.GetEmployeeSequenceIDAsync().Result.Substring(2)) + 1).ToString();
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            return await _employeeRepo.deleteEmployeeAsync(id);
        }

        public async Task<EmployeeDTO> GetEmployeeAsync(string id)
        {
            return _mapper.Map<EmployeeDTO>(await _employeeRepo.GetEmployeeByIdAsync(id));
        }

        public async Task<List<EmployeesDTO>> GetEmployeesAsync()
        {
            List<EmployeesDTO> employeesDTO = new List<EmployeesDTO>(); 
            foreach (var employee in await _employeeRepo.GetAllEmployeesAsync())
            {
                employeesDTO.Add(_mapper.Map<EmployeesDTO>(employee));
            }
            return employeesDTO;
        }

        public async Task<bool> UpdateEmployeeAsync(string id, EmployeeCreateUpdateDTO updatedEmployee)
        {
            Employee emp = _mapper.Map<Employee>(updatedEmployee, opt =>
               {
                   opt.Items["Empid"] = id;
               });

            return await _employeeRepo.UpdateEmployeeAsync(emp);
        }

        public async Task<List<EmployeesDTO>> GetFilteredEmployeesAsync(Filters filter)
        {
            List<EmployeesDTO> employeesDTO = new List<EmployeesDTO>();
            foreach (var employee in await _employeeRepo.GetFilteredEmployeesAsync(filter))
            {
                employeesDTO.Add(_mapper.Map<EmployeesDTO>(employee));
            }
            return employeesDTO;
        }
    }
}
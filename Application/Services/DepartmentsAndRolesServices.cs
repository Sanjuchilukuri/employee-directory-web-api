using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Interfaces;
using Infrastructure.Entities;

namespace Application.Services
{
    public class DepartmentsAndRolesServices : IDepartmentsAndRolesServices
    {
        private IDepartmentsAndRolesRepo _departmentsAndRolesRepo;
        private IMapper _mapper;

        public DepartmentsAndRolesServices(IDepartmentsAndRolesRepo departmentsAndRolesRepo, IMapper mapper)
        {
            _departmentsAndRolesRepo = departmentsAndRolesRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddRoleAsync(AddRoleDTO addRole)
        {
            Role role = _mapper.Map<Role>(addRole);
            return await _departmentsAndRolesRepo.AddRoleAsync(role);
        }

        public async Task<List<RolesDTO>> GetDepartmentRolesAsync(int departmentId)
        {
            List<RolesDTO> roles = new List<RolesDTO>();
            foreach (var role in await _departmentsAndRolesRepo.GetDeparmentRolesAsync(departmentId))
            {
                roles.Add(_mapper.Map<RolesDTO>(role));
            }
            return roles;
        } 
        public async Task<List<AllRolesDTO>> GetAllRolesAsync()
        {
            List<AllRolesDTO> allRoles = new List<AllRolesDTO>();
            foreach (var role in await _departmentsAndRolesRepo.GetAllRoles())
            {
                allRoles.Add(_mapper.Map<AllRolesDTO>(role));
            }
            return allRoles;
               
        }

        public async Task<List<DepartmentDTO>> GetDepartmentsAsync()
        {
            List<DepartmentDTO> departments = new List<DepartmentDTO>();
            foreach (var department in await _departmentsAndRolesRepo.GetDepartmentsAsync())
            {
                departments.Add(_mapper.Map<DepartmentDTO>(department));
            }
            return departments;
        }

        public async Task<bool> AddDepartmentAsync(AddDepartmentDTO addDepartment)
        {
            return await _departmentsAndRolesRepo.AddDepartmentAsync(_mapper.Map<Department>(addDepartment));
        }

    }
}
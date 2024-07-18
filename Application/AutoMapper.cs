using Application.DTO.AuthModels;
using Application.DTO;
using AutoMapper;
using Infrastructure.Entities;

namespace Application
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Role.DepartmentId));

            CreateMap<Employee, EmployeesDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Role.Dept.DepartmentName))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Name));


            CreateMap<EmployeeCreateUpdateDTO, Employee>()
            .ForMember(dest => dest.EmpId, opt => opt.MapFrom((src, dest, _, context) => context.Items["Empid"]));

            CreateMap<AddRoleDTO, Role>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AddDepartmentDTO, Department>();

            CreateMap<Department, DepartmentDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName));

            CreateMap<Role, RolesDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));

            CreateMap<LoginModel, User>();

            CreateMap<User, UserDTO>();

            CreateMap<RegisterModel, User>();

            CreateMap<Location, CommonDTO>();

            CreateMap<Manager, CommonDTO>();

            CreateMap<Project, CommonDTO>();

            CreateMap<Role, AllRolesDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Dept.DepartmentName));


        }
    }
}
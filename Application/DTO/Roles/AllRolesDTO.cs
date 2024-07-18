namespace Application.DTO
{
    public class AllRolesDTO
    {
        public string Name { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public ICollection<EmployeeDTO> Employees { get; set; } = null!;
    }
}
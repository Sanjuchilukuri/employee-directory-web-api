namespace Application.DTO
{
    public class EmployeeDTO : BaseEmployeeDTO
    {
        public string EmpId { get; set; } = string.Empty;

        public DateOnly? DateofBirth { get; set; }

        public string? PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public int DepartmentId { get; set; }

        public int LocationId { get; set; }

        public int? ManagerId { get; set; }

        public int? ProjectId { get; set; }
    }
}

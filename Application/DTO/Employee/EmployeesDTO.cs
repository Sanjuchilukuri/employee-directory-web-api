namespace Application.DTO
{
    public class EmployeesDTO : BaseEmployeeDTO
    {
        public string EmpId { get; set; } = String.Empty;

        public string Status { get; set; } = String.Empty;

        public string Department { get; set; } = String.Empty;

        public string Role { get; set; } = String.Empty;

        public string Location { get; set; } = String.Empty;

    }
}
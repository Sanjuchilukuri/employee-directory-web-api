using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class EmployeeCreateUpdateDTO : BaseEmployeeDTO
    {
        public DateOnly? DateofBirth { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int LocationId { get; set; }

        public int? ManagerId { get; set; }

        public int? ProjectId { get; set; }
    }
}
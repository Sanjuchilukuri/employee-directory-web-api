namespace Infrastructure.Entities
{
    public class Employee : BaseEntity
    {
        public string Image { get; set; } = String.Empty;

        public string EmpId { get; set; } = String.Empty;

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public DateOnly? DateofBirth { get; set; }

        public DateOnly JoiningDate { get; set; }

        public string Status { get; set; } = "Active";

        public string? PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public int LocationId { get; set; }

        public int ManagerId { get; set; }

        public int ProjectId { get; set; }

        public Role Role { get; set; } = null!;

        public Location Location { get; set; } = null!;

        public Manager Manager { get; set; } = null!;

        public Project Project { get; set; } = null!;
    }
}

namespace Infrastructure.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = String.Empty;

        public int DepartmentId { get; set; }

        public Department Dept { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}

namespace Infrastructure.Entities
{
    public class Department : BaseEntity
    { 
        public int Id { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public ICollection<Role> Roles { get; set; } = new List<Role>();

    }
}

namespace Infrastructure.Entities
{
    public class Project : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
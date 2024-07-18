namespace Infrastructure.Entities
{
    public class User : BaseEntity
    {

        public int Id { get; set; }
        
        public string Name { get; set; } = String.Empty;

        public string emailAddress { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string Role { get; set; } = String.Empty;
    }
}
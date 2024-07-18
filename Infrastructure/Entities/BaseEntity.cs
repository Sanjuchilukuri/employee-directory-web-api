namespace Infrastructure.Entities
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; } = Environment.UserName;

        public DateOnly CreatedOn { get; set; } = DateOnly.FromDateTime(DateTime.Now); 

        public string ModifiedBy { get; set; } = Environment.UserName;

        public DateOnly ModifiedOn { get; set;}  = DateOnly.FromDateTime(DateTime.Now); 
    }
}
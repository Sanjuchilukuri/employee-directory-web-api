using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
 
        public string Name { get; set; } = string.Empty;

        public string emailAddress { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
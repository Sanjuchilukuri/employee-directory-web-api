using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class AddRoleDTO
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

    }
}
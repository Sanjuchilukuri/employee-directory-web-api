using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class BaseEmployeeDTO
    {
        [Required]
        public string Image { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateOnly JoiningDate { get; set; }

    
    }
}

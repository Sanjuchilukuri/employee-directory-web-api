using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTO.AuthModels
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string emailAddress { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
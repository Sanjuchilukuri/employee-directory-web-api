using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTO.AuthModels
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string emailAddress { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        [JsonIgnore]
        public string Role { get; set; } = "User";
    }
}


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class AddDepartmentDTO
    {
        [Required]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
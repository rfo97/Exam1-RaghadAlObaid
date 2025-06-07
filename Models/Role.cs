using System.ComponentModel.DataAnnotations;

namespace Exam1.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Enter Role Name")]
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam1.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Enter User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Min 6 Char")]
        public string Password { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string City { get; set; }
        public Genders Gender { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public enum Genders { Male, Female }
    }
}

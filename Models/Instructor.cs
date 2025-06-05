using System.ComponentModel.DataAnnotations;

namespace Exam1.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter instructor name")]
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "ex: user@gmail.com")]
        [Required(ErrorMessage = "Enter Email Address")]
        public string Email { get; set; }
        public string Major { get; set; }
    }
}

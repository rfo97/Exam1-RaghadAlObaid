using System.ComponentModel.DataAnnotations;

namespace Exam1.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter student name")]
        [MaxLength(50, ErrorMessage = "Max 50 Char")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "ex: user@gmail.com")]
        [Required(ErrorMessage = "Enter Email Address")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}

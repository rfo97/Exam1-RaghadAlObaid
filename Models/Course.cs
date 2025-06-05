using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam1.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter course title")]
        [MinLength(3, ErrorMessage = "Min 3 Char")]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public int Capacity { get; set; }
        [Range(0, 999.99)]
        public decimal Price { get; set; }
        public int Duration { get; set; }
        
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}

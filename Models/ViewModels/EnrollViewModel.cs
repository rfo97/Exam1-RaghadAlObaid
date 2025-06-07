namespace Exam1.Models.ViewModels
{
    public class EnrollViewModel
    {
        public Student Student { get; set; }
        public List<Course> Courses { get; set; }
        public List<int> EnrolledCoursesIds { get; set; } = new();
    }
}

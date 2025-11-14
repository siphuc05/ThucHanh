namespace Lab4_CodeFirst.Models
{
    public class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        // Navigation property
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

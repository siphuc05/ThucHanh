namespace Lab4_CodeFirst.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int LearnerId { get; set; }
        public float? Grade { get; set; }
        
        public virtual Learner? Learner { get; set; }
        public virtual Course? Course { get; set; }
    }
}

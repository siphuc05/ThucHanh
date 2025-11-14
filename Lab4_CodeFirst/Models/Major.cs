namespace Lab4_CodeFirst.Models
{
    public class Major
    {

        public Major()
        {
            Learners = new HashSet<Learner>();
        }

        public int MajorId { get; set; }
        public string MajorName { get; set; }

        // Navigation property
        public ICollection<Learner> Learners { get; set; }
    }
}

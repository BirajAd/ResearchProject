namespace RPHost.Models
{
    public class AuthorResearch
    {
        public int AuthorResearchId { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int ResearchId { get; set; }
        public Research Research { get; set; }
    }
}
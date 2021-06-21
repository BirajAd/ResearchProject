using System;

namespace RPHost.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public User User{ get; set; }
        public int UserId { get; set; }
      
    }
}
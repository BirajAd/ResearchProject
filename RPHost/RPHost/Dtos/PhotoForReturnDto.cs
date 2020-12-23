using System;

namespace RPHost.Dtos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
using System;

namespace RPHost.Dtos
{
    public class PhotosForDetailedDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
    }
}
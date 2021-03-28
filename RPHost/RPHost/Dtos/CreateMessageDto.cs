using System;

namespace RPHost.Dtos
{
    public class CreateMessageDto
    {
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        // public DateTime Time { get; set; }
        // public CreateMessageDto()
        // {
        //     Time = DateTime.Now;
        // }
    }
}
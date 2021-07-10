namespace RPHost.GraphQL.Messages
{       
    // public record AddMessageInput(string recipientUsername, string content);

    public class AddMessageInput
    {
        public string RecipientUsername { get; set; } 
        public string Content { get; set; }
        
    }
}
namespace RPHost.Helpers
{
    public class MessageParams
    {
        private const int MaxPageSize = 50;
        // if not requested it always return the first page(=1)
        public int PageNumber { get; set; } = 1;
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            //we we have value which is greater than maxPagesize we return maxpagesize othwise we return value
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        
        public string Username { get; set; }
        public string Container { get; set; } = "All";
    }
}
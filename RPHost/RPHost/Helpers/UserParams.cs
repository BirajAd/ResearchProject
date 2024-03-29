namespace RPHost.Helpers
{
    public class UserParams
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
        
        public int UserId { get; set; }
        public string OrderBy { get; set; }
        public bool Followees { get; set; } = false;
        public bool Followers { get; set; } = false;
        

        
    }
}
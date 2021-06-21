namespace RPHost.Models
{
    public class Follow
    {
        public User Follower { get; set; }
        public int FollowerId { get; set; }

        public User Followee { get; set; }

        public int FolloweeId { get; set; }
    }
}
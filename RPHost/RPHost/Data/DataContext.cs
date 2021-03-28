using Microsoft.EntityFrameworkCore;
using RPHost.Models;

namespace RPHost.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Author> Authors { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<AuthorResearch> AuthorResearches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            builder.Entity<Follow>()
            .HasKey(k => new{k.FollowerId, k.FolloweeId});

            builder.Entity<Follow>()
            .HasOne(s => s.Follower)
            .WithMany(f=> f.FollowedUsers)
            .HasForeignKey(s=> s.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Follow>()
            .HasOne(s => s.Followee)
            .WithMany(f=> f.FollowByUsers)
            .HasForeignKey(s=> s.FolloweeId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

        }
        


    }
}
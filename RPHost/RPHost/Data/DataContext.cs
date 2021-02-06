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
    
    }
}
using Microsoft.EntityFrameworkCore;
using LiveTogether.Models;

namespace LiveTogether.Data
{
    public class LiveTogetherContext : DbContext
    {
        public LiveTogetherContext(DbContextOptions options) : base(options) { }


        public DbSet<User> Users { get; set; }
    }
}
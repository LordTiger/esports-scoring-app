using Microsoft.EntityFrameworkCore;
using ScoreCraftApi.Enities;

namespace ScoreCraftApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}

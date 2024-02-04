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
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }


       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
        modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany()
            .HasForeignKey(m => m.RefHomeTeam)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.GuestTeam)
            .WithMany()
            .HasForeignKey(m => m.RefGuestTeam)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.WinningTeam)
            .WithMany()
            .HasForeignKey(m => m.RefMatchWinner)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure many-to-many relationship
        modelBuilder.Entity<UserTeam>()
            .HasKey(ut => new { ut.RefUser, ut.RefTeam });

        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTeams)
            .HasForeignKey(ut => ut.RefUser);

        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.Team)
            .WithMany(t => t.UserTeams)
            .HasForeignKey(ut => ut.RefTeam);

            base.OnModelCreating(modelBuilder);
        }

    }
}

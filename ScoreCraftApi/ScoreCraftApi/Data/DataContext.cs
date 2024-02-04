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
            // Add cascading delete
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey(m => m.RefHomeTeam)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.GuestTeam)
                .WithMany()
                .HasForeignKey(m => m.RefGuestTeam)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.WinningTeam)
                .WithMany()
                .HasForeignKey(m => m.RefMatchWinner)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserTeam>()
                .HasKey(ut => new { ut.RefUser, ut.RefTeam });

            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ut => ut.RefUser)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(ut => ut.RefTeam)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Team>().HasData(
                new Team { RefTeam = 1, TeamName = "Team A", IsArchived = false },
                new Team { RefTeam = 2, TeamName = "Team B", IsArchived = false }
            );

            base.OnModelCreating(modelBuilder);
       }

    }
}

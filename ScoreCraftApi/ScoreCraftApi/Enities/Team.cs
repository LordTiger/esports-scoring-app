using Microsoft.EntityFrameworkCore;
using ScoreCraftApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoreCraftApi.Enities
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? RefTeam { get; set; }
        public string? TeamName { get; set; }
        public bool? IsArchived { get; set; }

        [NotMapped]
        public virtual int? TeamSize { get; set; }


        // Navigation Properties
        public virtual ICollection<Match>? Matches { get; set; }
        public virtual ICollection<UserTeam>? UserTeams { get; set; }
    }

    public class TeamBLL
    {
        private readonly DataContext _context;

        public TeamBLL(DataContext context) // Create Constuctor for Database context
        {
            _context = context;
        }

        public async Task<List<Team>> GetTeamsCollection()
        {
            var teams = await _context.Teams.AsNoTracking()
            .Include(m => m.Matches)  // Include the Matches navigation property
            .Select(t => new Team()
            {
                RefTeam = t.RefTeam,
                TeamName = t.TeamName,
                TeamSize = t.UserTeams!.Count,
                IsArchived = t.IsArchived,
                Matches = _context.Matches.AsNoTracking()
                .Select(m => new Match()
                {
                    RefMatch = m.RefMatch,
                    RefHomeTeam = m.RefHomeTeam,
                    RefGuestTeam = m.RefGuestTeam,
                    MatchDate = m.MatchDate,
                    RefMatchWinner = m.RefMatchWinner,
                    Format = m.Format,
                    BestOf = m.BestOf,
                    HomeTeam = m.HomeTeam ?? new Team() { },
                    GuestTeam = m.GuestTeam ?? new Team() { },
                    WinningTeam = m.WinningTeam ?? new Team() { },
                    MatchResults = m.MatchResults
                }).Where(m => m.RefHomeTeam == t.RefTeam || m.RefGuestTeam == t.RefTeam).ToList()
            }).Where(t => t.IsArchived == false  || t.IsArchived == null).ToListAsync();

            return teams;
        }

        public async Task<List<Team>> GetTeamsForLookup()
        {

            var teams = await _context.Teams.AsNoTracking()
            .Include(m => m.Matches)  // Include the Matches navigation property
            .Select(t => new Team()
            {
                RefTeam = t.RefTeam,
                TeamName = t.TeamName,
                IsArchived = t.IsArchived,
                TeamSize = t.UserTeams!.Count,
            }).ToListAsync();

            return teams.ToList();
        }

        public async Task<List<Team>> GetUserTeams(Guid RefUser)
        {

            var userTeams = await _context.UserTeams.AsNoTracking()
                .Where(w => w.RefUser == RefUser)
                .Select(s => s.Team)
                .ToListAsync();

            return userTeams;

        }   

        public async Task<Team> GetTeam(int? RefTeam)
        {
            var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(u => u.RefTeam == RefTeam);

            if (team is null) return null;

            return team;
        }

        public async Task<Team> InsertTeam(Team model)
        {
            _context.Teams.Add(model);
            await _context.SaveChangesAsync();

            return await GetTeam(model.RefTeam);
        }

        public async Task<User> AddUserToTeam(UserTeam model)
        {
            var user = await _context.Users
                .Include(u => u.UserTeams)
                .FirstOrDefaultAsync(u => u.RefUser == model.RefUser);

            if (user is null)
                return null;

            // Add new RefTeams
            foreach (var refTeam in model.RefTeams)
            {
                if (!user.UserTeams.Any(ut => ut.RefTeam == refTeam))
                {
                    var userTeam = new UserTeam
                    {
                        RefUser = model.RefUser,
                        RefTeam = refTeam
                    };

                    _context.UserTeams.Add(userTeam);
                }
            }

            // Remove RefTeams not present in the new list
            var teamsToRemove = user.UserTeams.Where(ut => !model.RefTeams.Contains((int)ut.RefTeam)).ToList();
            foreach (var teamToRemove in teamsToRemove)
            {
                _context.UserTeams.Remove(teamToRemove);
            }

            await _context.SaveChangesAsync();

            // Refresh user entity to reflect changes
            user = await new UsersBLL(_context).GetUser(model.RefUser);

            return user;
        }

        public async Task<Team> UpdateTeam(Team model)
        {
            await _context.Teams.Where(t => t.RefTeam == model.RefTeam).ExecuteUpdateAsync(n =>
                           n.SetProperty(t => t.TeamName, model.TeamName));

            return await GetTeam(model.RefTeam);
        }

        public async Task<bool?> DeleteTeam(int? RefTeam)
        {
            // Archive the team instead of deleting it, so that historical data is preserved
            var dbTeam = await _context.Teams.FindAsync(RefTeam);

            if (dbTeam is null)
                return null;

            dbTeam.IsArchived = true;

            await _context.SaveChangesAsync();

            return true;
        }

    }
}

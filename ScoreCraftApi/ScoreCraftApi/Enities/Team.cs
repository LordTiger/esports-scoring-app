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


        // Navigation Properties
        public ICollection<Match>? Matches { get; set; }
        public ICollection<UserTeam>? UserTeams { get; set; }
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
            return await _context.Teams.AsNoTracking().ToListAsync();
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
            var teamsToRemove = user.UserTeams.Where(ut => !model.RefTeams.Contains(ut.RefTeam)).ToList();
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
            await _context.Teams.Where(t => t.RefTeam == RefTeam).ExecuteDeleteAsync();

            return true;
        }

    }
}

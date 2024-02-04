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
        public required string TeamName { get; set; }


        // Navigation Properties
        public ICollection<User>? Members { get; set; }
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
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.RefUser == model.RefUser);
            //var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(u => u.RefTeam == RefTeam);

            if (user is null)
                return null;

            if(model.RefTeams.Any())
            {
                model.RefTeams.ForEach(refTeam => {
                    var userTeam = new UserTeam
                    {
                        RefUser = model.RefUser,
                        RefTeam = refTeam
                    };

                    _context.UserTeams.Add(userTeam);
                });

                await _context.SaveChangesAsync();
            }

            return await new UsersBLL(_context).GetUser(model.RefUser);
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

        public async Task<bool?> RemoveUserFromTeam(Guid RefUser, int RefTeam)
        {
            await _context.UserTeams.Where(u => u.RefUser == RefUser && u.RefTeam == RefTeam).ExecuteDeleteAsync();

            return true;
        }


    }
}

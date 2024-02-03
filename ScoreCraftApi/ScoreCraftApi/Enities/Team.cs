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
        public ICollection<User> Members { get; set; }
        public ICollection<Match> Matches { get; set; }
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
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeam(int? RefTeam)
        {
            var team = await _context.Teams.FindAsync(RefTeam);

            if (team is null) return null;

            return team;
        }

        public async Task<Team> InsertTeam(Team model)
        {
            _context.Teams.Add(model);
            await _context.SaveChangesAsync();

            return await GetTeam(model.RefTeam);
        }

        public async Task<Team> UpdateTeam(Team model)
        {
            var dbTeam = await _context.Teams.FindAsync(model.RefTeam);

            if (dbTeam is null)
                return null;

            dbTeam.TeamName = model.TeamName;

            await _context.SaveChangesAsync();

            return await GetTeam(model.RefTeam);
        }

        public async Task<bool?> DeleteTeam(int? RefTeam)
        {
            var dbTeam = await _context.Teams.FindAsync(RefTeam);

            if (dbTeam is null)
                return false;

            _context.Teams.Remove(dbTeam);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}

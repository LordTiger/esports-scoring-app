using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ScoreCraftApi.Data;
namespace ScoreCraftApi.Enities
{
    public class Match
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RefMatch { get; set; }
        public int? RefHomeTeam { get; set; }
        public int? RefGuestTeam { get; set; }
        public DateTime MatchDate { get; set; }
        public int? RefMatchWinner { get; set; }
        public string? Formate { get; set; } // 1v1, 2v2, etc.
        public int BestOf { get; set; } // 3, 5, etc.
        public ICollection<MatchResult>? MatchResults { get; set; }


        [ForeignKey("RefHomeTeam")]
        public virtual Team? HomeTeam { get; set; }

        [ForeignKey("RefGuestTeam")]
        public virtual Team? GuestTeam { get; set; }

        [ForeignKey("RefMatchWinner")]
        public virtual Team? WinningTeam { get; set; }
    }

    public class MatchesBLL
    {

        private readonly DataContext _context;

        public MatchesBLL(DataContext context) // Create Constuctor for Database context
        {
            _context = context;
        }

        public async Task<List<Match>> GetMatchesCollection()
        {
            return await _context.Matches.ToListAsync();
        }

        public async Task<Match> GetMatch(int RefMatch)
        {
            var match = await _context.Matches.FindAsync(RefMatch);

            if (match is null) return null;

            return match;
        }

        public async Task<Match> InsertMatch(Match model)
        {
            _context.Matches.Add(model);
            await _context.SaveChangesAsync();

            return await GetMatch(model.RefMatch);
        }

        public async Task<Match> UpdateMatch(Match model)
        {
            var dbMatch = await _context.Matches.FindAsync(model.RefMatch);

            if (dbMatch is null)
                return null;

            // Update properties as needed
            dbMatch.RefHomeTeam = model.RefHomeTeam;
            dbMatch.RefGuestTeam = model.RefGuestTeam;
            dbMatch.MatchDate = model.MatchDate;
            dbMatch.RefMatchWinner = model.RefMatchWinner;
            dbMatch.Formate = model.Formate;
            dbMatch.BestOf = model.BestOf;

            await _context.SaveChangesAsync();

            return await GetMatch(model.RefMatch);
        }

        public async Task<bool?> DeleteMatch(int RefMatch)
        {
            var dbMatch = await _context.Matches.FindAsync(RefMatch);

            if (dbMatch is null)
                return null;

            _context.Matches.Remove(dbMatch);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

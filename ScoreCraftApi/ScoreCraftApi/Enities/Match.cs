using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ScoreCraftApi.Data;
using Microsoft.AspNetCore.Components.Web.Virtualization;
namespace ScoreCraftApi.Enities
{
    public class Match
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? RefMatch { get; set; }
        public int? RefHomeTeam { get; set; }
        public int? RefGuestTeam { get; set; }
        public DateTime? MatchDate { get; set; }
        public int? RefMatchWinner { get; set; }
        public string? Format { get; set; } // Corrected property name
        public int? BestOf { get; set; }
        public bool? IsArchived { get; set; }
        public ICollection<MatchResult>? MatchResults { get; set; }

        [NotMapped]
        public virtual int HomeTotalWon
        {
            get
            {
                if (MatchResults == null)
                    return 0;

                return MatchResults.Count(mr => mr.HomeResult > mr.GuestResult);
            }
        }

        [NotMapped]
        public virtual int GuestTotalWon
        {
            get
            {
                if (MatchResults == null)
                    return 0;

                return MatchResults.Count(mr => mr.GuestResult > mr.HomeResult);
            }
        }

        public virtual Team? HomeTeam { get; set; }
        public virtual Team? GuestTeam { get; set; }
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
            //return await _context.Matches.ToListAsync();
            var matches = await _context.Matches.AsNoTracking()
                .Where(m => m.IsArchived == false)
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
                }).ToListAsync();

            return matches.Where(m => m.IsArchived == false || m.IsArchived == null).ToList();
        }

        public async Task<List<Match>> GetTeamMatchCollection(int? RefTeam)
        {
            //return await _context.Matches.ToListAsync();
            var matches = await _context.Matches.AsNoTracking()
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
                }).ToListAsync();

            return matches.Where(m => m.RefHomeTeam == RefTeam || m.RefGuestTeam == RefTeam).ToList() ?? [];
        }

        public async Task<Match> GetMatch(int? RefMatch)
        {
            var match = await _context.Matches.AsNoTracking()
                .Select(m => new Match()
                {
                    RefMatch = m.RefMatch,
                    RefHomeTeam = m.RefHomeTeam,
                    RefGuestTeam = m.RefGuestTeam,
                    MatchDate = m.MatchDate,
                    RefMatchWinner = m.RefMatchWinner,
                    IsArchived = m.IsArchived,
                    Format = m.Format,
                    BestOf = m.BestOf,
                    HomeTeam = m.HomeTeam ?? new Team() { },
                    GuestTeam = m.GuestTeam ?? new Team() { },
                    WinningTeam = m.WinningTeam ?? new Team() { },
                    MatchResults = m.MatchResults
                })
                .FirstOrDefaultAsync( m => m.RefMatch == RefMatch && m.IsArchived == false || m.IsArchived == null);

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
            dbMatch.Format = model.Format;
            dbMatch.BestOf = model.BestOf;

            await _context.SaveChangesAsync();

            return await GetMatch(model.RefMatch);
        }

        public async Task<bool?> DeleteMatch(int RefMatch)
        {
            // Archive the match instead of deleting it, so that historical data is preserved
            var dbMatch = await _context.Matches.FindAsync(RefMatch);

            if (dbMatch is null)
                return null;

            dbMatch.IsArchived = true;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

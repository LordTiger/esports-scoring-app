using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ScoreCraftApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ScoreCraftApi.Enities
{
    public class MatchResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RefMatchResult { get; set; }
        public int? RefMatch { get; set; }

        [ForeignKey("RefMatch")]
        public virtual Match Match { get; set; }

        public int? HomeResult { get; set; }
        public int? GuestResult { get; set; }
    }

    public class MatchResultsBLL
    {
        private readonly DataContext _context;

        public MatchResultsBLL(DataContext context) // Create Constuctor for Database context
        {
            _context = context;
        }

        public async Task<List<MatchResult>> GetMatchResultsCollection(int? RefMatch)
        {
            var matchResults = await _context.MatchResults.Where(mr => mr.RefMatch == RefMatch).ToListAsync();
            return matchResults;
        }

        public async Task<MatchResult> GetMatchResult(int? RefMatchResult)
        {
            var matchResult = await _context.MatchResults.FindAsync(RefMatchResult);

            if (matchResult is null) return null;

            return matchResult;
        }

        public async Task<MatchResult> InsertMatchResult(MatchResult model)
        {
            _context.MatchResults.Add(model);
            await _context.SaveChangesAsync();

            return await GetMatchResult(model.RefMatch);
        }

        public async Task<MatchResult> UpdateMatchResult(MatchResult model)
        {
            var dbMatchResult = await _context.MatchResults.FindAsync(model.RefMatchResult);

            if (dbMatchResult is null)
                return null;

            // Update properties as needed
            dbMatchResult.HomeResult = model.HomeResult;
            dbMatchResult.GuestResult = model.GuestResult;

            await _context.SaveChangesAsync();

            return await GetMatchResult(model.RefMatchResult);
        }

        public async Task<bool?> DeleteMatchResult(int RefMatchResult)
        {
            var dbMatchResult = await _context.MatchResults.FindAsync(RefMatchResult);

            if (dbMatchResult is null)
                return null;

            _context.MatchResults.Remove(dbMatchResult);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

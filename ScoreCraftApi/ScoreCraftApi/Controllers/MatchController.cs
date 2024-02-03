using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreCraftApi.Data;
using ScoreCraftApi.Enities;

namespace ScoreCraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly DataContext _context;

        public MatchController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetCollection")]
        public ActionResult<List<Match>> GetMatchCollection()
        {
            return Ok(new MatchesBLL(_context).GetMatchesCollection().Result);
        }

        [HttpGet("GetMatch")]
        public ActionResult<Match> GetMatch(int RefMatch)
        {
            var match =  new MatchesBLL(_context).GetMatch(RefMatch).Result;

            if (match is null) return NotFound("Match not found");
            return Ok(match);
        }

        [HttpGet("GetMatchResult")]
        public ActionResult<MatchResult> GetMatchResult(int RefMatch)
        {
            var match = new MatchResultsBLL(_context).GetMatchResult(RefMatch).Result;

            if (match is null) return NotFound("Match Result not found");
            return Ok(match);
        }

        [HttpPost("Insert")]
        public ActionResult<Match> Insert(Match model)
        {
            var newMatch = new MatchesBLL(_context).InsertMatch(model).Result;

            if (newMatch is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newMatch);
        }

        [HttpPut("Update")]
        public ActionResult<Match> Update(Match model)
        {
            var newMatch = new MatchesBLL(_context).UpdateMatch(model).Result;

            if (newMatch is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newMatch);
        }


        [HttpDelete("Delete")]
        public ActionResult<bool> Delete(int RefMatch)
        {
            var isMatchDeleted = new MatchesBLL(_context).DeleteMatch(RefMatch).Result;

            if (isMatchDeleted is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(isMatchDeleted);
        }
    }
}

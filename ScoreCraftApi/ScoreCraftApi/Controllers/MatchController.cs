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
        public async Task<ActionResult<List<Match>>> GetMatchCollection()
        {
            return Ok(await new MatchesBLL(_context).GetMatchesCollection());
        }

        [HttpGet("GetMatch")]
        public async Task<ActionResult<Match>> GetMatch(int RefMatch)
        {
            var match =  await new MatchesBLL(_context).GetMatch(RefMatch);

            if (match is null) return NotFound("Match not found");
            return Ok(match);
        }

        [HttpGet("GetMatchResult")]
        public async Task<ActionResult<MatchResult>> GetMatchResult(int RefMatch)
        {
            var match = await new MatchResultsBLL(_context).GetMatchResult(RefMatch);

            if (match is null) return NotFound("Match Result not found");
            return Ok(match);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<Match>> Insert(Match model)
        {
            var newMatch = await new MatchesBLL(_context).InsertMatch(model);

            if (newMatch is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newMatch);
        }

        [HttpPost("AddMatchResult")]
        public async Task<ActionResult<MatchResult>> AddMatchResult(MatchResult model)
        {
            var newMatchResult = await new MatchResultsBLL(_context).InsertMatchResult(model);

            if (newMatchResult is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newMatchResult);
        }


        [HttpPut("Update")]
        public async Task<ActionResult<Match>> Update(Match model)
        {
            var newMatch = await new MatchesBLL(_context).UpdateMatch(model);

            if (newMatch is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newMatch);
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> Delete(int RefMatch)
        {
            var isMatchDeleted = await new MatchesBLL(_context).DeleteMatch(RefMatch);

            if (isMatchDeleted is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(isMatchDeleted);
        }
    }
}

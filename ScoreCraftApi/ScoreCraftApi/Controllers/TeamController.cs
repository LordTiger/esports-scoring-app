using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreCraftApi.Data;
using ScoreCraftApi.Enities;

namespace ScoreCraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly DataContext _context;

        public TeamController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetCollection")]
        public ActionResult<List<Team>> GetTeamsCollection()
        {
            return Ok( new TeamBLL(_context).GetTeamsCollection().Result);
        }

        [HttpGet("GetTeam")]
        public ActionResult<Team> GetTeam(int RefTeam)
        {
            var team = new TeamBLL(_context).GetTeam(RefTeam).Result;

            if (team is null) return NotFound("Team not found");
            return Ok(team);
        }

        [HttpPost("Insert")]
        public  ActionResult<Team> Insert(Team model)
        {
            var newTeam = new TeamBLL(_context).InsertTeam(model).Result;

            if (newTeam is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newTeam);
        }

        [HttpPut("Update")]
        public ActionResult<Team> Update(Team model)
        {
            var newTeam = new TeamBLL(_context).UpdateTeam(model).Result;

            if (newTeam is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newTeam);
        }

        [HttpDelete("Delete")]
        public ActionResult<bool> Delete(int RefTeam)
        {
            var isTeamDeleted = new TeamBLL(_context).DeleteTeam(RefTeam).Result;

            if (isTeamDeleted is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(isTeamDeleted);
        }
    }
}

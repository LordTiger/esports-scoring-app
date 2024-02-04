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
        public async Task<ActionResult<List<Team>>> GetTeamsCollection()
        {
            return Ok( await new TeamBLL(_context).GetTeamsCollection());
        }

        [HttpGet("GetTeam")]
        public async Task<ActionResult<Team>> GetTeam(int RefTeam)
        {
            var team = await new TeamBLL(_context).GetTeam(RefTeam);

            if (team is null) return NotFound("Team not found");
            return Ok(team);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<Team>> Insert(Team model)
        {
            var newTeam = await new TeamBLL(_context).InsertTeam(model);

            if (newTeam is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newTeam);
        }

        [HttpPost("AddUserToTeam")]
        public async Task<ActionResult<User>> AddUserToTeam(UserTeam model)
        {
            var newTeam = await new TeamBLL(_context).AddUserToTeam(model);

            if (newTeam is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newTeam);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Team>> Update(Team model)
        {
            var newTeam = await new TeamBLL(_context).UpdateTeam(model);

            if (newTeam is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newTeam);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> Delete(int RefTeam)
        {
            var isTeamDeleted = await new TeamBLL(_context).DeleteTeam(RefTeam);

            if (isTeamDeleted is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(isTeamDeleted);
        }

        [HttpDelete("RemoveUserFromTeam")]
        public async Task<ActionResult<bool>> RemoveUserFromTeam(Guid RefUser, int RefTeam)
        {
            var isUserRemoved = await new TeamBLL(_context).RemoveUserFromTeam(RefUser, RefTeam);

            if (isUserRemoved is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(isUserRemoved);
        }
    }
}

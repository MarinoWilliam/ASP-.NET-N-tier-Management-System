using Microsoft.AspNetCore.Mvc;
using NTier.ManagementSystem.Data.Entities;
using NTier.ManagementSystem.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace NTier.Management_System.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teams = await _teamService.GetAllTeamsAsync();
                return Ok(teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var team = await _teamService.GetTeamByIdAsync(id);
                if (team == null)
                    return NotFound();
                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Team team)
        {
            try
            {
                if (team == null) return BadRequest("Team data is required.");

                var createdTeam = await _teamService.AddTeamAsync(team);
                return CreatedAtAction(nameof(Get), new { id = createdTeam.Id }, createdTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Team team)
        {
            try
            {
                if (team == null || team.Id != id) return BadRequest("Invalid team data.");

                var updatedTeam = await _teamService.UpdateTeamAsync(team);
                if (updatedTeam == null) return NotFound();

                return Ok(updatedTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _teamService.DeleteTeamAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

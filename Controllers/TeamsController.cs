using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Logs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TeamsController(ITeamService teamService,ILoggerManager logger,IMapper mapper)
        {
            _teamService = teamService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            try
            {
                _logger.LogInfo("Getting all teams");
                var teams = await _teamService.GetAllTeamsAsync();
                var teamDtos = _mapper.Map<IEnumerable<TeamDto>>(teams);
                return Ok(teamDtos);
            }
            catch (Exception ex)
            {
                Exception error = ex;
                _logger.LogError(ex.Message.ToString());
                if (error.InnerException != null)
                {
                    error = error.InnerException;
                    _logger.LogError(error.Message.ToString());
                }
                return Ok(error.Message.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            try
            {

                _logger.LogInfo($"Getting team with ID {id}");
                var team = await _teamService.GetTeamByIdAsync(id);
                if (team == null)
                {
                    return NotFound();
                }
                var teamDto = _mapper.Map<TeamDto>(team);
                return Ok(teamDto);
            }
            catch (Exception ex)
            {
                Exception error = ex;
                _logger.LogError(ex.Message.ToString());
                if (error.InnerException != null)
                {
                    error = error.InnerException;
                    _logger.LogError(error.Message.ToString());
                }
                return Ok(error.Message.ToString());
            }

        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeam(TeamDto teamDto)
        {
            try
            {

                _logger.LogInfo("Creating a new team");
                var team = _mapper.Map<Team>(teamDto);
                var createdTeam = await _teamService.CreateTeamAsync(teamDto);
                var createdTeamDto = _mapper.Map<TeamDto>(createdTeam);
                return CreatedAtAction(nameof(GetTeam), new { id = createdTeamDto.TeamID }, createdTeamDto);
            }
            catch (Exception ex)
            {
                Exception error = ex;
                _logger.LogError(ex.Message.ToString());
                if (error.InnerException != null)
                {
                    error = error.InnerException;
                    _logger.LogError(error.Message.ToString());
                }
                return Ok(error.Message.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, TeamDto teamDto)
        {
            try
            {

                _logger.LogInfo($"Updating team with ID {id}");
                if (id != teamDto.TeamID)
                {
                    return BadRequest();
                }
                var team = _mapper.Map<Team>(teamDto);
                var result = await _teamService.UpdateTeamAsync(id, teamDto);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                Exception error = ex;
                _logger.LogError(ex.Message.ToString());
                if (error.InnerException != null)
                {
                    error = error.InnerException;
                    _logger.LogError(error.Message.ToString());
                }
                return Ok(error.Message.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {

                _logger.LogInfo($"Deleting team with ID {id}");
                var result = await _teamService.DeleteTeamAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                Exception error = ex;
                _logger.LogError(ex.Message.ToString());
                if (error.InnerException != null)
                {
                    error = error.InnerException;
                    _logger.LogError(error.Message.ToString());
                }
                return Ok(error.Message.ToString());
            }
        }
    }
}

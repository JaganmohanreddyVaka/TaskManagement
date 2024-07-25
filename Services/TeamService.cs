using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _context.Teams.ToListAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task<TeamDto> GetTeamByIdAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<TeamDto> CreateTeamAsync(TeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<bool> UpdateTeamAsync(int teamId, TeamDto teamDto)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                return false;
            }

            _mapper.Map(teamDto, team);
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null)
            {
                return false;
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

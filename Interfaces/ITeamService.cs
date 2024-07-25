using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto> GetTeamByIdAsync(int teamId);
        Task<TeamDto> CreateTeamAsync(TeamDto teamDto);
        Task<bool> UpdateTeamAsync(int teamId, TeamDto teamDto);
        Task<bool> DeleteTeamAsync(int teamId);
    }
}

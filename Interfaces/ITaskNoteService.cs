using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskNoteService
    {
        Task<IEnumerable<TaskNoteDto>> GetAllTaskNotesAsync();
        Task<TaskNoteDto> GetTaskNoteByIdAsync(int taskNoteId);
        Task<TaskNoteDto> CreateTaskNoteAsync(TaskNoteDto taskNoteDto);
        Task<bool> UpdateTaskNoteAsync(int taskNoteId, TaskNoteDto taskNoteDto);
        Task<bool> DeleteTaskNoteAsync(int taskNoteId);
    }
}

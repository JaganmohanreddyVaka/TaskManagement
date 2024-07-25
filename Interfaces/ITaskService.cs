using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
    
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(int taskId);
        Task<TaskDto> CreateTaskAsync(TaskDto taskDto);
        Task<bool> UpdateTaskAsync(int taskId, TaskDto taskDto);
        Task<bool> DeleteTaskAsync(int taskId);
    }
}

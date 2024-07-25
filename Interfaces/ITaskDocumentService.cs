using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskDocumentService
    {
        Task<IEnumerable<TaskDocumentDto>> GetAllTaskDocumentsAsync();
        Task<TaskDocumentDto> GetTaskDocumentByIdAsync(int taskDocumentId);
        Task<TaskDocumentDto> CreateTaskDocumentAsync(TaskDocumentDto taskDocumentDto);
        Task<bool> UpdateTaskDocumentAsync(int taskDocumentId, TaskDocumentDto taskDocumentDto);
        Task<bool> DeleteTaskDocumentAsync(int taskDocumentId);
    }

}

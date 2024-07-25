using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{

    public class TaskDocumentService : ITaskDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskDocumentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDocumentDto>> GetAllTaskDocumentsAsync()
        {
            var taskDocuments = await _context.TaskDocuments.ToListAsync();
            return _mapper.Map<IEnumerable<TaskDocumentDto>>(taskDocuments);
        }

        public async Task<TaskDocumentDto> GetTaskDocumentByIdAsync(int taskDocumentId)
        {
            var taskDocument = await _context.TaskDocuments.FindAsync(taskDocumentId);
            return _mapper.Map<TaskDocumentDto>(taskDocument);
        }

        public async Task<TaskDocumentDto> CreateTaskDocumentAsync(TaskDocumentDto taskDocumentDto)
        {
            var taskDocument = _mapper.Map<TaskDocument>(taskDocumentDto);
            _context.TaskDocuments.Add(taskDocument);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDocumentDto>(taskDocument);
        }

        public async Task<bool> UpdateTaskDocumentAsync(int taskDocumentId, TaskDocumentDto taskDocumentDto)
        {
            var taskDocument = await _context.TaskDocuments.FindAsync(taskDocumentId);
            if (taskDocument == null)
            {
                return false;
            }

            _mapper.Map(taskDocumentDto, taskDocument);
            _context.TaskDocuments.Update(taskDocument);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskDocumentAsync(int taskDocumentId)
        {
            var taskDocument = await _context.TaskDocuments.FindAsync(taskDocumentId);
            if (taskDocument == null)
            {
                return false;
            }

            _context.TaskDocuments.Remove(taskDocument);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

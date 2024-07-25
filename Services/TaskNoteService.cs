using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{

    public class TaskNoteService : ITaskNoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaskNoteService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskNoteDto>> GetAllTaskNotesAsync()
        {
            var taskNotes = await _context.TaskNotes.ToListAsync();
            return _mapper.Map<IEnumerable<TaskNoteDto>>(taskNotes);
        }

        public async Task<TaskNoteDto> GetTaskNoteByIdAsync(int taskNoteId)
        {
            var taskNote = await _context.TaskNotes.FindAsync(taskNoteId);
            return _mapper.Map<TaskNoteDto>(taskNote);
        }

        public async Task<TaskNoteDto> CreateTaskNoteAsync(TaskNoteDto taskNoteDto)
        {
            var taskNote = _mapper.Map<TaskNote>(taskNoteDto);
            _context.TaskNotes.Add(taskNote);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskNoteDto>(taskNote);
        }

        public async Task<bool> UpdateTaskNoteAsync(int taskNoteId, TaskNoteDto taskNoteDto)
        {
            var taskNote = await _context.TaskNotes.FindAsync(taskNoteId);
            if (taskNote == null)
            {
                return false;
            }

            _mapper.Map(taskNoteDto, taskNote);
            _context.TaskNotes.Update(taskNote);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskNoteAsync(int taskNoteId)
        {
            var taskNote = await _context.TaskNotes.FindAsync(taskNoteId);
            if (taskNote == null)
            {
                return false;
            }

            _context.TaskNotes.Remove(taskNote);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

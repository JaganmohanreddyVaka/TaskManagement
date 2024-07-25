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
    public class TaskNotesController : ControllerBase
    {
        private readonly ITaskNoteService _taskNoteService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TaskNotesController(ITaskNoteService taskNoteService, ILoggerManager logger, IMapper mapper)
        {
            _taskNoteService = taskNoteService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskNoteDto>>> GetTaskNotes()
        {
            try
            {
                _logger.LogInfo("Getting all task notes");
                var taskNotes = await _taskNoteService.GetAllTaskNotesAsync();
                var taskNoteDtos = _mapper.Map<IEnumerable<TaskNoteDto>>(taskNotes);
                return Ok(taskNoteDtos);
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
        public async Task<ActionResult<TaskNoteDto>> GetTaskNote(int id)
        {
            try
            {
                _logger.LogInfo($"Getting task note with ID {id}");
                var taskNote = await _taskNoteService.GetTaskNoteByIdAsync(id);
                if (taskNote == null)
                {
                    return NotFound();
                }
                var taskNoteDto = _mapper.Map<TaskNoteDto>(taskNote);
                return Ok(taskNoteDto);
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
        public async Task<ActionResult<TaskNoteDto>> CreateTaskNote(TaskNoteDto taskNoteDto)
        {
            try
            {
                _logger.LogInfo("Creating a new task note");
                var taskNote = _mapper.Map<TaskNote>(taskNoteDto);
                var createdTaskNote = await _taskNoteService.CreateTaskNoteAsync(taskNoteDto);
                var createdTaskNoteDto = _mapper.Map<TaskNoteDto>(createdTaskNote);
                return CreatedAtAction(nameof(GetTaskNote), new { id = createdTaskNoteDto.TaskNoteID }, createdTaskNoteDto);
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
        public async Task<IActionResult> UpdateTaskNote(int id, TaskNoteDto taskNoteDto)
        {
            try
            {
                if (id != taskNoteDto.TaskNoteID)
                {
                    return BadRequest();
                }
                var taskNote = _mapper.Map<TaskNote>(taskNoteDto);
                var result = await _taskNoteService.UpdateTaskNoteAsync(id,taskNoteDto);
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
        public async Task<IActionResult> DeleteTaskNote(int id)
        {
            try
            {
                _logger.LogInfo($"Deleting task note with ID {id}");
                var result = await _taskNoteService.DeleteTaskNoteAsync(id);
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

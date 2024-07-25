using AutoMapper;
using Azure;
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
    public class TaskDocumentsController : ControllerBase
    {
        private readonly ITaskDocumentService _taskDocumentService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public TaskDocumentsController(ITaskDocumentService taskDocumentService, ILoggerManager logger,IMapper mapper)
        {
            _taskDocumentService = taskDocumentService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDocumentDto>>> GetTaskDocuments()
        {
            try
            {
                _logger.LogInfo("Getting all task documents");
                var taskDocuments = await _taskDocumentService.GetAllTaskDocumentsAsync();
                var taskDocumentDtos = _mapper.Map<IEnumerable<TaskDocumentDto>>(taskDocuments);
                return Ok(taskDocumentDtos);
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
        public async Task<ActionResult<TaskDocumentDto>> GetTaskDocument(int id)
        {
            try
            {
                _logger.LogInfo($"Getting task document with ID {id}");
                var taskDocument = await _taskDocumentService.GetTaskDocumentByIdAsync(id);
                if (taskDocument == null)
                {
                    return NotFound();
                }
                var taskDocumentDto = _mapper.Map<TaskDocumentDto>(taskDocument);
                return Ok(taskDocumentDto);
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
        public async Task<ActionResult<TaskDocumentDto>> CreateTaskDocument(TaskDocumentDto taskDocumentDto)
        {
            try
            {


                _logger.LogInfo("Creating a new task document");
                var taskDocument = _mapper.Map<TaskDocument>(taskDocumentDto);
                var createdTaskDocument = await _taskDocumentService.CreateTaskDocumentAsync(taskDocumentDto);
                var createdTaskDocumentDto = _mapper.Map<TaskDocumentDto>(createdTaskDocument);
                return CreatedAtAction(nameof(GetTaskDocument), new { id = createdTaskDocumentDto.TaskDocumentID }, createdTaskDocumentDto);
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
        public async Task<IActionResult> UpdateTaskDocument(int id, TaskDocumentDto taskDocumentDto)
        {
            try
            {

                _logger.LogInfo($"Updating task document with ID {id}");
                if (id != taskDocumentDto.TaskDocumentID)
                {
                    return BadRequest();
                }
                var taskDocument = _mapper.Map<TaskDocument>(taskDocumentDto);
                var result = await _taskDocumentService.UpdateTaskDocumentAsync(id, taskDocumentDto);
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
        public async Task<IActionResult> DeleteTaskDocument(int id)
        {
            try
            {
                _logger.LogInfo($"Deleting task document with ID {id}");
                var result = await _taskDocumentService.DeleteTaskDocumentAsync(id);
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

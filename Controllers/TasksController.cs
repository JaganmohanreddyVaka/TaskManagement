using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Logs;
using TaskManagementSystem.Models;
using Task = TaskManagementSystem.Entities.Task;


namespace TaskManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TasksController(ITaskService taskService,ILoggerManager logger, IMapper mapper)
        {
            _taskService = taskService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            try
            {
                _logger.LogInfo("");
                var tasks = await _taskService.GetAllTasksAsync();
                var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks);
                return Ok(taskDtos);
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
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            try
            {
                _logger.LogInfo("");
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                var taskDto = _mapper.Map<TaskDto>(task);
                return Ok(taskDto);
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
        public async Task<ActionResult<TaskDto>> CreateTask(TaskDto taskDto)
        {
            try
            {

                _logger.LogInfo("");
                var task = await _taskService.CreateTaskAsync(taskDto);
                var createdTaskDto = _mapper.Map<TaskDto>(CreateTask);
                return CreatedAtAction(nameof(GetTask), new { id = task.TaskID }, createdTaskDto);
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
        public async Task<IActionResult> UpdateTask(int id, TaskDto taskDto)
        {
            try
            {
                _logger.LogInfo("");
                var task = _mapper.Map<Task>(taskDto);
                var result = await _taskService.UpdateTaskAsync(id, taskDto);
        

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
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                _logger.LogInfo("");
                var result = await _taskService.DeleteTaskAsync(id);
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

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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, ILoggerManager logger,IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {

                _logger.LogInfo("Getting all users");
                var users = await _userService.GetAllUsersAsync();
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(userDtos);
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
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {

                _logger.LogInfo($"Getting user with ID {id}");
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
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
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            try
            {

                _logger.LogInfo("Creating a new user");
                var user = _mapper.Map<User>(userDto);
                var createdUser = await _userService.CreateUserAsync(userDto);
                var createdUserDto = _mapper.Map<UserDto>(createdUser);
                return CreatedAtAction(nameof(GetUser), new { id = createdUserDto.UserID }, createdUserDto);
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
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            try
            {
                _logger.LogInfo($"Updating user with ID {id}");
                if (id != userDto.UserID)
                {
                    return BadRequest();
                }
                var user = _mapper.Map<User>(userDto);
                var result = await _userService.UpdateUserAsync(id,userDto);
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {

                _logger.LogInfo("");
                var result = await _userService.DeleteUserAsync(id);
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

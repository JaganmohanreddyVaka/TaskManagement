using AutoMapper;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Models;
using Task = TaskManagementSystem.Entities.Task;

namespace TaskManagementSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Task, TaskDto>().ReverseMap();
            CreateMap<TaskNote, TaskNoteDto>().ReverseMap();
            CreateMap<TaskDocument, TaskDocumentDto>().ReverseMap();
        }
    }
}

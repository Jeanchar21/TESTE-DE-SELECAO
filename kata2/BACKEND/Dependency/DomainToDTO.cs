using AutoMapper;
using BACKEND.Models;
using BACKEND.DTO;

namespace BACKEND.Dependency
{
    public class DomainToDTO : Profile
    {
        public DomainToDTO()
        {
            CreateMap<TaskListRequestDTO, Tarefa>().ReverseMap();
            CreateMap<TaskUpdateStatusDTO, Tarefa>().ReverseMap();
        }
    }
}

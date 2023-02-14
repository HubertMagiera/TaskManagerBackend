using AutoMapper;
using ToDoBackend.Entities;

namespace ToDoBackend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User_DTO, User>();
            CreateMap<Task_DTO,Entities.Task>();
            CreateMap<Task_type_DTO, Task_type>();
        }
    }
}

using AutoMapper;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.DTO_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User_DTO, View_user>();
            CreateMap<Task_DTO, View_task>()
                .ForMember(view => view.user_id, map => map.MapFrom(dto => dto.user.user_id))
                .ForMember(view => view.user_name, map => map.MapFrom(dto => dto.user.user_name))
                .ForMember(view => view.user_surname, map => map.MapFrom(dto => dto.user.user_surname));

            CreateMap<Task_type_DTO, View_task_type>();
            CreateMap<Create_Task, Task_DTO>();
            CreateMap<Create_User, User_DTO>();
           
        }
    }
}

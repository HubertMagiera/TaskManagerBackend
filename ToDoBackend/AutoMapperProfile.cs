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
            //creating mapping profiles for classes
            //in case dto and non dto models have the same structure, CreateMap<base, destination>() is sufficient
            //in case names or strucutre is different, need to add .ForMember() to manually specify appropriate fields
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

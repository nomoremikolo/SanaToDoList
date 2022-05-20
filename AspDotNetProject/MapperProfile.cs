using AspDotNetProject.GraphQL.Categories;
using AspDotNetProject.GraphQL.ToDo;
using AspDotNetProject.GraphQL.ToDo.Inputs;
using AutoMapper;
using BusinessLogic.Entities;

namespace AspDotNetProject
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TaskEntity, NewTaskInput>().ReverseMap();
            CreateMap<TaskEntity, UpdateTaskInput>().ReverseMap();

            CreateMap<CategoryEntity, NewCategoryInput>().ReverseMap();
            CreateMap<CategoryEntity, UpdateCategoryInput>().ReverseMap();
        }
    }
}

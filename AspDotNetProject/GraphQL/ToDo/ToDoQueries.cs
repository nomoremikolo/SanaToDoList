using BusinessLogic.intefaces;
using TaskEntity = BusinessLogic.Entities.TaskEntity;
using GraphQL.Types;
using AspDotNetProject.GraphQL.Types;
using GraphQL;

namespace AspDotNetProject.GraphQL.ToDo
{
    public class ToDoQueries : ObjectGraphType
    {
        private readonly IRepository repository;

        public ToDoQueries(IRepository repository)
        {
            this.repository = repository;

            Field<NonNullGraphType<ListGraphType<ToDoType>>, List<TaskEntity>>()
                .Name("GetAllTasks")
                .Resolve(ctx =>
                {
                    return repository.GetAllTasksList();
                });
            Field<NonNullGraphType<ToDoType>, TaskEntity>()
                .Name("GetTaskById")
                .Argument<NonNullGraphType<IntGraphType>, int>("id", "Task id")
                .Resolve(ctx =>
                {
                    return repository.GetTaskById(ctx.GetArgument<int>("id"));
                });
                
        }
    }
}

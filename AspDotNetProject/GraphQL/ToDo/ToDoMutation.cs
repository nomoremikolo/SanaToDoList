using AspDotNetProject.GraphQL.ToDo.Inputs;
using AspDotNetProject.GraphQL.Types;
using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.intefaces;
using GraphQL;
using GraphQL.Types;

namespace AspDotNetProject.GraphQL.ToDo
{
    public class ToDoMutation : ObjectGraphType
    {
        private IRepository repository;
        public ToDoMutation(IRepository repository, IMapper mapper)
        {
            this.repository = repository;

            Field<ToDoType, TaskEntity>()
                .Name("Create")
                .Argument<NonNullGraphType<NewTaskInputType>, NewTaskInput>("NewTask", "New task arguments")
                .Resolve(ctx =>
                {
                    var input = ctx.GetArgument<NewTaskInput>("NewTask");
                    var task = mapper.Map<TaskEntity>(input);

                    return repository.CreateTask(task);
                });

            Field<ToDoType, TaskEntity>()
                 .Name("Update")
                 .Argument<NonNullGraphType<UpdateTaskInputType>, UpdateTaskInput>("UpdateTask", "Task to update")
                 .Resolve(ctx =>
                 {
                     var input = ctx.GetArgument<UpdateTaskInput>("UpdateTask");
                     var task = mapper.Map<TaskEntity>(input);

                     return repository.UpdateTask(task);
                 });
            Field<ToDoType, TaskEntity>()
                .Name("Remove")
                .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Task id to remove")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");
                    return repository.DeleteTask(id);
                });
        }
    }
}

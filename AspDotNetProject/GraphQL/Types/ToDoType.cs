using GraphQL.Types;
using TaskEntity = BusinessLogic.Entities.TaskEntity;

namespace AspDotNetProject.GraphQL.Types
{
    public class ToDoType : ObjectGraphType<TaskEntity>
    {
        public ToDoType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsCompleted")
                .Resolve(ctx => ctx.Source.IsCompleted);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("CategoryId")
                .Resolve(ctx => ctx.Source.CategoryId);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Text")
                .Resolve(ctx => ctx.Source.Text);

            Field<DateTimeGraphType, DateTime>()
                .Name("CreatedAt")
                .Resolve(ctx => ctx.Source.CreatedAt);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadLine")
                .Resolve(ctx => ctx.Source.DeadLine);

            Field<DateTimeGraphType, DateTime?>()
                .Name("CompletedAt")
                .Resolve(ctx => ctx.Source.CompletedAt);
        }
    }
}

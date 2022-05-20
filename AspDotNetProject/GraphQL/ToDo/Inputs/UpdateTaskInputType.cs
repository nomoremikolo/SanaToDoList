using BusinessLogic.intefaces;
using GraphQL.Types;

namespace AspDotNetProject.GraphQL.ToDo
{
    public class UpdateTaskInputType : InputObjectGraphType<UpdateTaskInput>
    {
        private IRepository repository;
        public UpdateTaskInputType(IRepository repository)
        {
            this.repository = repository;

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Text")
                .Resolve(ctx => ctx.Source.Text);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadLine")
                .Resolve(ctx => ctx.Source.DeadLine);

            Field<BooleanGraphType, bool>()
                .Name("IsCompleted")
                .Resolve(ctx => ctx.Source.IsCompleted);

            Field<DateTimeGraphType, DateTime?>()
                .Name("CompletedAt")
                .Resolve(ctx => ctx.Source.CompletedAt);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("CategoryId")
                .Resolve(ctx => ctx.Source.CategoryId);

        }
    }
}
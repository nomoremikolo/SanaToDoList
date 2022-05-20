using GraphQL.Types;

namespace AspDotNetProject.GraphQL.ToDo.Inputs
{
    public class NewTaskInputType : InputObjectGraphType<NewTaskInput>
    {
        public NewTaskInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Text")
                .Resolve(ctx => ctx.Source.Text);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadLine")
                .Resolve(ctx => ctx.Source.DeadLine);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("CategoryId")
                .Resolve(ctx => ctx.Source.CategoryId);
        }
    }
}

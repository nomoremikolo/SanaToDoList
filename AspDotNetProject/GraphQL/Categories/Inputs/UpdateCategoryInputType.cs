using GraphQL.Types;

namespace AspDotNetProject.GraphQL.Categories
{
    public class UpdateCategoryInputType : InputObjectGraphType<UpdateCategoryInput>
    {
        public UpdateCategoryInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Text")
                .Resolve(ctx => ctx.Source.Text);
        }
    }
}
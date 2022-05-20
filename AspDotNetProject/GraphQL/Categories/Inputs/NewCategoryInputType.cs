using GraphQL.Types;

namespace AspDotNetProject.GraphQL.Categories
{
    public class NewCategoryInputType : InputObjectGraphType<NewCategoryInput>
    {
        public NewCategoryInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Text")
                .Resolve(ctx => ctx.Source.Text);
        }
    }
}
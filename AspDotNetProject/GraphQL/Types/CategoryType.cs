using BusinessLogic.Entities;
using GraphQL.Types;

namespace AspDotNetProject.GraphQL
{
    public class CategoryType : ObjectGraphType<CategoryEntity>
    {
        public CategoryType()
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
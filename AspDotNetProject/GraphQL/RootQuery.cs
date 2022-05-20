using AspDotNetProject.GraphQL.ToDo;
using GraphQL.Types;

namespace AspDotNetProject.GraphQL
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<ToDoQueries>()
                .Name("ToDo")
                .Resolve(_ => new { });
            Field<CategoriesQueries>()
                .Name("Categories")
                .Resolve(_ => new { });
        }
    }
}

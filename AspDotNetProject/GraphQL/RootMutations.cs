using AspDotNetProject.GraphQL.Categories;
using AspDotNetProject.GraphQL.ToDo;
using GraphQL.Types;

namespace AspDotNetProject.GraphQL
{
    public class RootMutations : ObjectGraphType
    {
        public RootMutations()
        {
            Field<ToDoMutation>()
                .Name("ToDo")
                .Resolve(_ => new { });
            
            Field<CategoriesMutation>()
                .Name("Categories")
                .Resolve(_ => new { });
        }
    }
}
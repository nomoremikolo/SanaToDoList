using GraphQL.Types;

namespace AspDotNetProject.GraphQL
{
    public class ToDoSchema : Schema
    {
        public ToDoSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            //Mutation = provider.GetRequiredService<>();
        }
    }
}

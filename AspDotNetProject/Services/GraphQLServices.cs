using AspDotNetProject.GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL;
using GraphQL.Types;
using GraphQL.Server;

namespace AspDotNetProject.Extensions
{
    public static class GraphQLServices
    {
        public static IServiceCollection AddGraphQLApi(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<ToDoListSchema>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.UnhandledExceptionDelegate = (context) =>
                {
                    Console.WriteLine("StackTrace:" + context.Exception.StackTrace);
                    Console.WriteLine("Message:" + context.Exception.Message);
                    context.ErrorMessage = context.Exception.Message;
                };
            })
                .AddSystemTextJson()
                .AddGraphTypes(typeof(ToDoListSchema));

            services.AddSingleton<RootQuery>();
            services.AddSingleton<RootMutations>();
            return services;
        }
    }
}

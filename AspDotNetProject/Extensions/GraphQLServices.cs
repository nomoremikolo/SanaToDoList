using AspDotNetProject.GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.MicrosoftDI;
using GraphQL;
using GraphQL.Server;
using GraphQL.NewtonsoftJson;
using AspDotNetProject.GraphQL.ToDo;
using AspDotNetProject.GraphQL.Types;

namespace AspDotNetProject.Extensions
{
    public static class GraphQLServices
    {
        public static IServiceCollection AddGraphQLApi(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<ToDoSchema>();
            services.AddSingleton<ToDoQueries>();
            services.AddTransient<ToDoType>();


            services.AddGraphQL(options => options
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddHttpMiddleware<ToDoSchema>()
                .AddSystemTextJson()
                .AddNewtonsoftJson()
                .AddSchema<ToDoSchema>()
                );

            services.AddTransient<RootQuery>();

            return services;
        }
    }
}

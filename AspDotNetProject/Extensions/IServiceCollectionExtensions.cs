using AspDotNetProject.GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.MicrosoftDI;
using GraphQL;
using GraphQL.Server;
using GraphQL.NewtonsoftJson;

namespace AspDotNetProject.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddGraphQLApi(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<ToDoSchema>();

            services.AddGraphQL(options => options
                .AddHttpMiddleware<ToDoSchema>()
                .AddSystemTextJson()
                .AddNewtonsoftJson()
                .AddSchema<ToDoSchema>()
                );

            services.AddScoped<RootQuery>();

            return services;
        }
    }
}

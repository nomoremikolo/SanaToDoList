using AspDotNetProject.GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.MicrosoftDI;
using GraphQL;
using GraphQL.Server;
using GraphQL.NewtonsoftJson;
using AspDotNetProject.GraphQL.ToDo;
using AspDotNetProject.GraphQL.Types;
using AspDotNetProject.GraphQL.ToDo.Inputs;
using AspDotNetProject.GraphQL.Categories;

namespace AspDotNetProject.Extensions
{
    public static class GraphQLServices
    {
        public static IServiceCollection AddGraphQLApi(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddTransient<RootQuery>();
            services.AddTransient<RootMutations>();

            services.AddSingleton<ToDoListSchema>();

            services.AddSingleton<ToDoQueries>();
            services.AddSingleton<ToDoMutation>();

            services.AddSingleton<ToDoType>();
            services.AddSingleton<CategoryType>();

            services.AddSingleton<NewTaskInputType>();
            services.AddSingleton<NewCategoryInputType>();

            services.AddSingleton<UpdateTaskInputType>();
            services.AddSingleton<UpdateCategoryInputType>();

            services.AddSingleton<CategoriesQueries>();
            services.AddSingleton<CategoriesMutation>();


            services.AddGraphQL(options => options
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddHttpMiddleware<ToDoListSchema>()
                .AddSystemTextJson()
                .AddNewtonsoftJson()
                .AddSchema<ToDoListSchema>()
                );



            return services;
        }
    }
}

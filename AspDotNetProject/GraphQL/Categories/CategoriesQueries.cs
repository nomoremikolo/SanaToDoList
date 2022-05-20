using BusinessLogic.Entities;
using BusinessLogic.intefaces;
using GraphQL;
using GraphQL.Types;

namespace AspDotNetProject.GraphQL
{
    public class CategoriesQueries : ObjectGraphType
    {
        private readonly IRepository repository;

        public CategoriesQueries(IRepository repository)
        {
            this.repository = repository;

            Field<NonNullGraphType<ListGraphType<CategoryType>>, List<CategoryEntity>>()
                .Name("GetAllCategories")
                .Resolve(ctx =>
                {
                    return repository.GetAllCategoriesList();
                });

            Field<NonNullGraphType<CategoryType>, CategoryEntity>()
                .Name("GetCategoryById")
                .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Task id")
                .Resolve(ctx =>
                {
                    return repository.GetCategoryById(ctx.GetArgument<int>("id"));
                });
        }
    }
}
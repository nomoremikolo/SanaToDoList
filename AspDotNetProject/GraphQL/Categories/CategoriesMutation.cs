using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.intefaces;
using GraphQL;
using GraphQL.Types;

namespace AspDotNetProject.GraphQL.Categories
{
    public class CategoriesMutation : ObjectGraphType
    {
        private IRepository repository;
        public CategoriesMutation(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            Field<CategoryType, CategoryEntity>()
                .Name("Create")
                .Argument<NonNullGraphType<NewCategoryInputType>, NewCategoryInput>("NewCategory", "New category arguments")
                .Resolve(ctx =>
                {
                    var input = ctx.GetArgument<NewCategoryInput>("NewCategory");
                   
                    var category = mapper.Map<CategoryEntity>(input);

                    return repository.CreateCategory(category);
                });

            Field<CategoryType, CategoryEntity>()
                 .Name("Update")
                 .Argument<NonNullGraphType<UpdateCategoryInputType>, UpdateCategoryInput>("UpdateCategory", "Category to update")
                 .Resolve(ctx =>
                 {
                     var input = ctx.GetArgument<UpdateCategoryInput>("UpdateCategory");

                     var category = mapper.Map<CategoryEntity>(input);

                     return repository.UpdateCategory(category);
                 });

            Field<CategoryType, CategoryEntity>()
                .Name("Remove")
                .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Category id to remove")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");
                    return repository.DeleteCategory(id);
                });
        }
    }
}

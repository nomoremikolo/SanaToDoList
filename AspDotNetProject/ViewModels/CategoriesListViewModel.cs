using BusinessLogic.Entities;
namespace AspDotNetProject.ViewModels
{
    public class CategoriesListViewModel
    {
        public List<CategoryEntity> Categories { get; set; }
        public CategoryEntity CategoryModel { get; set; }
    }
}

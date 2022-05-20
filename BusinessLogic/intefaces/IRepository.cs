using System.Threading.Tasks;
using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.intefaces
{
    public interface IRepository
    {
        List<TaskEntity> GetAllTasksList();
        TaskEntity CreateTask(TaskEntity task);
        TaskEntity GetTaskById(int id);
        TaskEntity UpdateTask(TaskEntity task);
        TaskEntity DeleteTask(int id);

        List<CategoryEntity> GetAllCategoriesList();
        CategoryEntity CreateCategory(CategoryEntity category);
        CategoryEntity GetCategoryById(int id);
        CategoryEntity UpdateCategory(CategoryEntity category);
        CategoryEntity DeleteCategory(int id);
    }
}

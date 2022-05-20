using System.Threading.Tasks;
using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.intefaces
{
    public interface IRepository
    {
       List<TaskEntity> GetAllTasksList();
        void CreateTask(TaskEntity task);
        TaskEntity GetTaskById(int id);
        void UpdateTask(TaskEntity task);
        void DeleteTask(int id);

        List<CategoryEntity> GetAllCategoriesList();
        void CreateCategory(CategoryEntity category);
        CategoryEntity GetCategoryById(int id);
        void UpdateCategory(CategoryEntity category);
        void DeleteCategory(int id);
    }
}

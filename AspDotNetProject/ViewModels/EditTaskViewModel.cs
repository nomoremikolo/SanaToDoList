using BusinessLogic.Entities;
using TaskEntity = BusinessLogic.Entities.TaskEntity;

namespace AspDotNetProject.ViewModels
{
    public class EditTaskViewModel
    {
        public TaskEntity TaskModel { get; set; }
        public List<CategoryEntity> Categories { get; set; }
    }
}

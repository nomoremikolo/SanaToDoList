using BusinessLogic.Entities;
using BusinessLogic.intefaces;
using TaskEntity = BusinessLogic.Entities.TaskEntity;

namespace AspDotNetProject.ViewModels
{
    public class TaskListViewModel
    {
        public IEnumerable<TaskEntity> CompleteTasksTable { get; set; }
        public IEnumerable<TaskEntity> UnCompleteTasksTable { get; set; }
        public List<CategoryEntity> Categories { get; set; }
        public TaskEntity TaskModel { get; set; }
        public IRepository Repository { get; set; }
    }
}

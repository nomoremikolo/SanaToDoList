using AspDotNetProject.Enum;
using AspDotNetProject.Models;
using AspDotNetProject.ViewModels;
using BusinessLogic.intefaces;
using Microsoft.AspNetCore.Mvc;


namespace AspDotNetProject.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly IRepository repository;
        public ToDoListController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TaskListViewModel
            {
                CompleteTasksTable = Enumerable.Where(repository.GetAllTasksList(), t => t.IsCompleted).OrderBy(t => t.CompletedAt).Reverse(),
                UnCompleteTasksTable = Enumerable.Where(repository.GetAllTasksList(), t => !t.IsCompleted).OrderBy(t => t.DeadLine),
                Categories = repository.GetAllCategoriesList(),
                TaskModel = new BusinessLogic.Entities.TaskEntity(),
                Repository = repository
            });
        }

        [HttpPost]
        public IActionResult Create(TaskListViewModel model)
        {
            if (model.TaskModel.Text != string.Empty)
            {
                repository.CreateTask(model.TaskModel);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View(new EditTaskViewModel
            {
                TaskModel = repository.GetTaskById(Id),
                Categories = repository.GetAllCategoriesList()
            });
        }
        
        [HttpPost]
        public IActionResult Edit(EditTaskViewModel model)
        {
            
            var task = model.TaskModel;
            var oldtask = repository.GetTaskById(task.Id);

            task.CreatedAt = oldtask.CreatedAt;
            task.IsCompleted = oldtask.IsCompleted;
            task.CompletedAt = oldtask.CompletedAt;

            repository.UpdateTask(task);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            repository.DeleteTask(Id);
            return RedirectToAction("Index");
        }

        public IActionResult Complete(int id)
        {
            var task = repository.GetTaskById(id);
            task.IsCompleted = task.IsCompleted ? false : true;
            task.CompletedAt = DateTime.Now;

            repository.UpdateTask(task);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DataBaseProvider(string identificator)
        {
            switch (identificator)
            {
                case "mssql":
                    Models.DataBaseProvider.DBIndetificator = (int)DataBaseEnum.MSSQL;
                    break;
                case "xml":
                    Models.DataBaseProvider.DBIndetificator = (int)DataBaseEnum.XML;
                    break;

                default:
                    break;
            }
            return RedirectToAction("Index");
        }
    }
}

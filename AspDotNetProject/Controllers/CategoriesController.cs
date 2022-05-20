using AspDotNetProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Entities;
using BusinessLogic.intefaces;

namespace AspDotNetProject.Controllers
{
    public class CategoriesController : Controller
    {
        public IRepository repository;

        public CategoriesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CategoriesListViewModel
            {
                CategoryModel = new CategoryEntity(),
                Categories = repository.GetAllCategoriesList()
            });
        }
        
        [HttpPost]
        public IActionResult Create(CategoriesListViewModel model)
        {
            repository.CreateCategory(model.CategoryModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View(repository.GetCategoryById(Id));
        }

        [HttpPost]
        public IActionResult Edit(CategoryEntity category)
        {
            repository.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            repository.DeleteCategory(Id);
            return RedirectToAction("Index");
        }
    }
}

using BusinessLogic.Entities;
using BusinessLogic.intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskEntity = BusinessLogic.Entities.TaskEntity;

namespace XMLDataRepository
{
    public class XMLRepository : IRepository
    {
        private readonly string categoriesPath = Environment.CurrentDirectory + @"\../XMLRepository\Storage\Categories.xml";
        private readonly string tasksPath = Environment.CurrentDirectory+ @"\../XMLRepository\Storage\Tasks.xml";
        public void CreateCategory(CategoryEntity category)
        {
            XDocument doc = XDocument.Load(categoriesPath);

            XElement todos = doc.Element("Categories");
            if (todos == null)
            {
                return;
            }

            todos.Add(new XElement("Category",
                    new XAttribute("Id", DateTime.Now.Ticks / 10 % 1000000000),
                    new XElement("Text", category.Text)
                ));
            doc.Save(categoriesPath);
        }

        public void CreateTask(TaskEntity task)
        {
            XDocument doc = XDocument.Load(tasksPath);
            XElement todos = doc.Element("Todos");
            if (todos == null)
            {
                return;
            }
            
            todos.Add(new XElement("Task",
                    new XAttribute("Id", DateTime.Now.Ticks / 10 % 1000000000),
                    new XElement("Text", task.Text),
                    new XElement("DeadLine", task.DeadLine),
                    new XElement("CreatedAt", DateTime.Now),
                    new XElement("CompletedAt", task.CompletedAt),
                    new XElement("IsCompleted", false),
                    new XElement("CategoryId", task.CategoryId)
                ));
            doc.Save(tasksPath);
        }

        public void DeleteCategory(int Id)
        {
            XDocument doc = XDocument.Load(categoriesPath);
            XElement todos = doc.Element("Categories");

            if (todos == null)
            {
                return;
            }

            var categoryItem = todos.Elements("Category").FirstOrDefault(t => t.Attribute("Id").Value == Id.ToString());

            if (categoryItem == null)
            {
                return;
            }

            var taskList = GetAllTasksList();

            if (taskList.FirstOrDefault(r => r.CategoryId == Id) != null)
            {
                return;
            }

            categoryItem.Remove();
            doc.Save(categoriesPath);
        }

        public void DeleteTask(int Id)
        {
            XDocument doc = XDocument.Load(tasksPath);
            XElement todos = doc.Element("Todos");

            if (todos == null)
            {
                return;
            }

            var target = todos.Elements("Task").FirstOrDefault(t => t.Attribute("Id").Value == Id.ToString());

            if (target == null)
            {
                return;
            }
            target.Remove();
            doc.Save(tasksPath);
        }

        public List<CategoryEntity> GetAllCategoriesList()
        {
            XDocument doc = XDocument.Load(categoriesPath);
            var list = new List<CategoryEntity>();

            XElement categories = doc.Element("Categories");

            if (categories == null)
            {
                return null;
            }

            foreach (XElement item in categories.Elements("Category"))
            {
                CategoryEntity category = new CategoryEntity();

                category.Id = Convert.ToInt32(item.Attribute("Id").Value);
                category.Text = Convert.ToString(item.Element("Text").Value);

                list.Add(category);
            }
            return list;
        }

        public List<TaskEntity> GetAllTasksList()
        {
            XDocument doc = XDocument.Load(tasksPath);
            var list = new List<TaskEntity>();

            XElement todos = doc.Element("Todos");
            if (todos == null)
            {
                return null;
            }

            foreach (XElement task in todos.Elements("Task"))
            {
                TaskEntity newTask = new TaskEntity();

                newTask.Id = Convert.ToInt32(task.Attribute("Id").Value);
                newTask.Text = Convert.ToString(task.Element("Text").Value);

                newTask.IsCompleted = Convert.ToBoolean(task.Element("IsCompleted").Value);
                newTask.CategoryId = Convert.ToInt32(task.Element("CategoryId").Value);

                newTask.CreatedAt = Convert.ToDateTime(task?.Element("CreatedAt")?.Value);
                

                if (task.Element("CompletedAt").Value == "")
                {
                    newTask.DeadLine = null;
                }
                else
                {
                    newTask.CompletedAt = Convert.ToDateTime(task?.Element("CompletedAt")?.Value);
                }

                if (task.Element("DeadLine").Value == "")
                {
                    newTask.DeadLine = null;
                }       
                else
                {
                    newTask.DeadLine = Convert.ToDateTime(task?.Element("DeadLine")?.Value);
                }

                list.Add(newTask);
            }
            list.Reverse();
            return list;
        }

        public CategoryEntity GetCategoryById(int id)
        {
            return GetAllCategoriesList().Where(r => r.Id == id).FirstOrDefault();
        }

        public TaskEntity GetTaskById(int id)
        {
            return GetAllTasksList().Where(r => r.Id == id).FirstOrDefault();
        }

        public void UpdateCategory(CategoryEntity category)
        {
            XDocument doc = XDocument.Load(categoriesPath);
            var editableCategory = doc.Element("Categories").Elements("Category").FirstOrDefault(t => t.Attribute("Id").Value == category.Id.ToString());

            if (editableCategory == null)
            {
                return;
            }
            editableCategory.Element("Text").Value = category.Text.ToString();

            doc.Save(categoriesPath);
        }

        public void UpdateTask(TaskEntity task)
        {
            XDocument doc = XDocument.Load(tasksPath);
            var editableTask = doc.Element("Todos").Elements("Task").FirstOrDefault(t => t.Attribute("Id").Value == task.Id.ToString());

            if (editableTask == null)
            {
                return;
            }
            editableTask.Element("Text").Value = task.Text.ToString();
            editableTask.Element("DeadLine").Value = task.DeadLine.ToString();
            editableTask.Element("CreatedAt").Value = task.CreatedAt.ToString();
            editableTask.Element("CompletedAt").Value = task.CompletedAt.ToString();
            editableTask.Element("IsCompleted").Value = task.IsCompleted.ToString();
            editableTask.Element("CategoryId").Value = task.CategoryId.ToString();

            doc.Save(tasksPath);
        }
    }
}

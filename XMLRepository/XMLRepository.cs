using BusinessLogic.Entities;
using BusinessLogic.intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskEntity = BusinessLogic.Entities.TaskEntity;
using AutoMapper;

namespace XMLDataRepository
{
    public class XMLRepository : IRepository
    {
        private readonly string categoriesPath = Environment.CurrentDirectory + @"\../XMLRepository\Storage\Categories.xml";
        private readonly string tasksPath = Environment.CurrentDirectory+ @"\../XMLRepository\Storage\Tasks.xml";
        public CategoryEntity CreateCategory(CategoryEntity category)
        {
            XDocument doc = XDocument.Load(categoriesPath);

            XElement todos = doc.Element("Categories");
            if (todos == null)
            {
                return null;
            }
            category.Id = (int)DateTime.Now.Ticks / 10 % 1000000000;
            todos.Add(new XElement("Category",
                    new XAttribute("Id", category.Id),
                    new XElement("Text", category.Text)
                ));
            doc.Save(categoriesPath);
            return category;
        }

        public TaskEntity CreateTask(TaskEntity task)
        {
            XDocument doc = XDocument.Load(tasksPath);
            XElement todos = doc.Element("Todos");
            if (todos == null)
            {
                return null;
            }
            task.Id = (int)(DateTime.Now.Ticks / 10 % 1000000000);
            task.CreatedAt = DateTime.Now;
            task.IsCompleted = false;

            todos.Add(new XElement("Task",
                    new XAttribute("Id", task.Id),
                    new XElement("Text", task.Text),
                    new XElement("DeadLine", task.DeadLine),
                    new XElement("CreatedAt", task.CreatedAt),
                    new XElement("CompletedAt", task.CompletedAt),
                    new XElement("IsCompleted", task.IsCompleted),
                    new XElement("CategoryId", task.CategoryId)
                ));
            doc.Save(tasksPath);
            return task;
        }

        public CategoryEntity DeleteCategory(int Id)
        {
            XDocument doc = XDocument.Load(categoriesPath);
            XElement todos = doc.Element("Categories");

            if (todos == null)
            {
                return null;
            }

            var categoryItem = todos.Elements("Category").FirstOrDefault(t => t.Attribute("Id").Value == Id.ToString());

            if (categoryItem == null)
            {
                return null;
            }

            var taskList = GetAllTasksList();

            if (taskList.AsEnumerable().FirstOrDefault(r => r.CategoryId == Id) != null)
            {
                return null;
            }

            categoryItem.Remove();
            doc.Save(categoriesPath);

            return GetCategoryById(Id);
        }

        public TaskEntity DeleteTask(int Id)
        {
            XDocument doc = XDocument.Load(tasksPath);
            XElement todos = doc.Element("Todos");

            if (todos == null)
            {
                return null;
            }

            var target = todos.Elements("Task").FirstOrDefault(t => t.Attribute("Id").Value == Id.ToString());

            if (target == null)
            {
                return null;
            }
            target.Remove();
            doc.Save(tasksPath);

            return GetTaskById(Id);
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
            return GetAllCategoriesList().AsEnumerable().Where(r => r.Id == id).FirstOrDefault();
        }

        public TaskEntity GetTaskById(int id)
        {
            return GetAllTasksList().AsEnumerable().Where(r => r.Id == id).FirstOrDefault();
        }

        public CategoryEntity UpdateCategory(CategoryEntity category)
        {
            XDocument doc = XDocument.Load(categoriesPath);
            var editableCategory = doc.Element("Categories").Elements("Category").FirstOrDefault(t => t.Attribute("Id").Value == category.Id.ToString());

            if (editableCategory == null)
            {
                return null;
            }
            editableCategory.Element("Text").Value = category.Text.ToString();

            doc.Save(categoriesPath);
            return category;
        }

        public TaskEntity UpdateTask(TaskEntity task)
        {
            XDocument doc = XDocument.Load(tasksPath);
            var editableTask = doc.Element("Todos").Elements("Task").FirstOrDefault(t => t.Attribute("Id").Value == task.Id.ToString());

            if (editableTask == null)
            {
                return null;
            }
            editableTask.Element("Text").Value = task.Text.ToString();
            editableTask.Element("DeadLine").Value = task.DeadLine.ToString();
            editableTask.Element("CompletedAt").Value = task.CompletedAt.ToString();
            editableTask.Element("IsCompleted").Value = task.IsCompleted.ToString();
            editableTask.Element("CategoryId").Value = task.CategoryId.ToString();

            doc.Save(tasksPath);

            return task;
        }
    }
}

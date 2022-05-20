using BusinessLogic.Entities;
using BusinessLogic.intefaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace MSQLDataRepository
{
    public class MSSqlRepository : IRepository
    {
        private readonly string connectionString;
        public MSSqlRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public CategoryEntity CreateCategory(CategoryEntity category)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                category.Id = connection.QuerySingle(
                    @"insert into Categories
                    (Text) 
                    VALUES 
                    (@CategoryText);
                    SELECT CAST(SCOPE_IDENTITY() as int);",
                    new
                    {
                        @CategoryText = category.Text
                    });
            }
            return category;
        }

        public TaskEntity CreateTask(TaskEntity task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var test = new TaskEntity();
                connection.Open();
                task.Id = connection.QuerySingle<int>(
                    @"insert into tasks 
                    (Text,DeadLine,CategoryId,CreatedAt) 
                    VALUES 
                    (@TaskText,@DeadLine,@CategoryId,@CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int);",
                    new
                    {
                        TaskText = task.Text,
                        DeadLine = task.DeadLine,
                        CategoryId = task.CategoryId,
                        CreatedAt = DateTime.Now,
                    }
                    );
            }

            return task;
        }

        public CategoryEntity DeleteCategory(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(
                    @"delete from Categories
                    where 
                    (Id) = @Id",
                    new
                    {
                        Id = id
                    });
            }
            return GetCategoryById(id);
        }

        public TaskEntity DeleteTask(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(
                    @"delete from Tasks 
                    where 
                    (Id) = @Id",
                    new
                    {
                        Id = id
                    });
            }
            return GetTaskById(id);
        }

        public List<CategoryEntity> GetAllCategoriesList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<CategoryEntity>(
                    @"select * from Categories 
                    order by Text");
                return result.ToList();
            }
        }

        public List<TaskEntity> GetAllTasksList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<TaskEntity>(
                    @"select * from Tasks");

                return result.ToList();
            }
        }

        public CategoryEntity GetCategoryById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<CategoryEntity>(
                    @"select * from Categories
                    where Id = @Id",
                    new
                    {
                        Id = id
                    }).FirstOrDefault();

                return result;
            }
        }

        public TaskEntity GetTaskById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<TaskEntity>(
                    @"select * from Tasks 
                    where Id = @Id",
                    new
                    {
                        Id = id
                    }).FirstOrDefault();

                return result;
            }
        }

        public CategoryEntity UpdateCategory(CategoryEntity category)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Execute(
                    @"UPDATE Categories
                    SET 
                    Text = @Text
                    WHERE 
                    Id = @Id",
                    new
                    {
                        Text = category.Text,
                        Id = category.Id
                    });
            }
            return category;
        }

        public TaskEntity UpdateTask(TaskEntity task)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QuerySingle(
                    @"UPDATE Tasks 
                    SET 
                    Text = @Text, 
                    IsCompleted = @IsCompleted, 
                    DeadLine = @DeadLine, 
                    CompletedAt = @CompletedAt,
                    CategoryId = @CategoryId
                    WHERE 
                    Id = @Id",
                    new
                    {
                        Id = task.Id,
                        Text = task.Text,
                        IsCompleted = task.IsCompleted,
                        DeadLine = task.DeadLine,
                        CompletedAt = task.CompletedAt,
                        CategoryId = task.CategoryId,
                        FinishDate = task.CompletedAt
                    });
            }
            return task;
        }
    }
}

namespace AspDotNetProject.GraphQL.ToDo
{
    public class UpdateTaskInput
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? DeadLine { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
    }
}
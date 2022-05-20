namespace AspDotNetProject.GraphQL.ToDo.Inputs
{
    public class NewTaskInput
    {
        public string Text { get; set; }
        public DateTime? DeadLine { get; set; }
        public int CategoryId { get; set; }
    }
}

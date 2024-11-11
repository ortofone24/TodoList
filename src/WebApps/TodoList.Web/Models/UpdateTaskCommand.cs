namespace TodoList.Web.Models
{
    public class UpdateTaskCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

namespace TodoList.Web.Models
{
    public class CreateTaskCommand
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
    }
}

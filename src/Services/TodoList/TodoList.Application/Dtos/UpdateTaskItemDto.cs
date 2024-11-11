namespace TodoList.Application.Dtos
{
    public record UpdateTaskItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

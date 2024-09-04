namespace TodoLists.Server.DataTransferObjects;

public record GetTodo(int TodoId, int TodoListId, string Description, DateTime?
    DueDate, bool IsImportant, bool IsComplete);

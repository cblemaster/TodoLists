namespace TodoLists.Server.DataTransferObjects;

public record GetTodo(int TodoId, int TodoListId, string Description, DateOnly?
    DueDate, bool IsImportant, bool IsComplete);

namespace TodoLists.Server.DataTransferObjects;

public record GetTodoList(int TodoListId, string Name, IEnumerable<GetTodo> Todos);

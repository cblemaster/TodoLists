
namespace TodoLists.API.Data.Models;

internal sealed class CreateTodoList
{
    internal Guid Id { get; set; }
    internal string Name { get; set; } = string.Empty;
}

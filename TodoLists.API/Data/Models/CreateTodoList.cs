
namespace TodoLists.API.Data.Models;

internal sealed class CreateTodoList
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

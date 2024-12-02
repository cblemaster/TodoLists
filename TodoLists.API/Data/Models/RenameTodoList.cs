
namespace TodoLists.API.Data.Models;

internal sealed class RenameTodoList
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

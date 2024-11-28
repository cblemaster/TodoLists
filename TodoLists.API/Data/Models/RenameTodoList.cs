
namespace TodoLists.API.Data.Models;

internal sealed class RenameTodoList
{
    internal Guid Id { get; set; }
    internal string Name { get; set; } = string.Empty;
}

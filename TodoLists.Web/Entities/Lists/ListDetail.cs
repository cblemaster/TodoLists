
using TodoLists.Web.Entities.Todos;

namespace TodoLists.Web.Entities.Lists;

public class ListDetail : ListBase
{
    public required int ListId { get; set; }
    public ICollection<Todo> Todos { get; set; } = [];
}

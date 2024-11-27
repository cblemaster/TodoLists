using TodoLists.Web.Entities.Lists;

namespace TodoLists.Web.Models.Lists;

internal sealed class ListSummary : ListBase
{
    public required int ListId { get; set; }
    public required int CountOfTodosNotComplete { get; set; }
}

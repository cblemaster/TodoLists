
namespace TodoLists.Domain;

public abstract class Entity
{
    public Guid Id { get; init; }  // TODO: I'm not crazy about this because the same generic name has to be used by descendants
}


namespace TodoLists.API.Data.Constants;

internal static class Constants
{
    internal const string CREATED_SUCCESSFULLY = "Created successfully.";
    internal const string RENAMED_SUCCESSFULLY = "Renameded successfully.";
    internal const string DELETED_SUCCESSFULLY = "Deleted successfully.";
    internal const string TOGGLED_SUCCESSFULLY = "Toggled successfully.";
    internal const string UPDATED_SUCCESSFULLY = "Updated successfully.";
    internal const string MOVED_SUCCESSFULLY = "Moved successfully.";
    internal const string CANNOT_DELETE_IMPORTANT_TODOS = "Cannot delete important todos.";
    internal const string CANNOT_DELETE_TODO_LISTS_WITH_TODOS_NOT_COMPLETE = "Cannot delete todo list because it contains todos that are not complete.";
    // TODO >> What other constants are floating around out there? Max length for list name and todo description...
    
    // TODO >> Not crazy about consts and methods together here - split methods off into extensions class
    internal static string FormattedValidationErrors(IEnumerable<string> errors) => $"The following validation errors occurred:\n{string.Join('\n', errors)}";
    internal static string FormattedError(string error) => $"The following error occurred:\n{error}";
    internal static string NotFound<T>() => $"{typeof(T)} not found.";
}

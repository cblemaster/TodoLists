
namespace TodoLists.API.Data.Constants;

internal static class Constants
{
    internal const int LIST_NAME_MAX_LENGTH = 255;
    internal const int TODO_DESC_MAX_LENGTH = 255;

    internal const string WELCOME = "Welcome to Todo Lists!";
    internal const string CONN_STRING_NOT_FOUND = "Error retrieving connection string.";

    internal const string CREATED_SUCCESSFULLY = "Created successfully.";
    internal const string CREATE_LIST_ERROR = "An unknown error occured creating the list.";
    internal const string CREATE_TODO_ERROR = "An unknown error occured creating the todo.";

    internal const string RENAMED_SUCCESSFULLY = "Renameded successfully.";
    internal const string UPDATED_SUCCESSFULLY = "Updated successfully.";
    internal const string RENAME_LIST_ERROR = "An unknown error occured renaming the list.";
    internal const string UPDATE_TODO_ERROR = "An unknown error occured updating the todo.";

    internal const string DELETED_SUCCESSFULLY = "Deleted successfully.";
    internal const string CANNOT_DELETE_TODO_LISTS_WITH_TODOS_NOT_COMPLETE = "Cannot delete todo list because it contains todos that are not complete.";
    internal const string CANNOT_DELETE_IMPORTANT_TODOS = "Cannot delete important todos.";
    internal const string DELETE_LIST_ERROR = "An unknown error occured deleting the list.";
    internal const string DELETE_TODO_ERROR = "An unknown error occured deleting the todo.";

    internal const string TOGGLED_SUCCESSFULLY = "Toggled successfully.";
    internal const string TOGGLE_IMPORTANCE_ERROR = "An unknown error occured toggling the todo's importance.";
    internal const string TOGGLE_COMPLETION_ERROR = "An unknown error occured toggling the todo's completion.";

    internal const string MOVED_SUCCESSFULLY = "Moved successfully.";
    internal const string MOVE_ERROR = "An unknown error occured moving the todo to a different list.";

    internal const string NO_SUMMARIES_FOUND = "No summaries found.";
    internal const string LIST_DETAIL_NOT_FOUND = "List detail not found.";
    internal const string TODO_NOT_FOUND = "Todo not found.";
    internal const string LIST_NOT_FOUND = "List not found.";
    internal const string DUE_TODAY_NOT_FOUND = "No todos due today found.";
    internal const string IMPORTANT_NOT_FOUND = "No important todos found.";
    internal const string COMPLETED_NOT_FOUND = "No completed todos found.";

    internal const string VALIDATION_ERROR_ROOT = "The following error occurred:\n";
    internal const string ERROR_ROOT = "The following validation errors occurred:\n";
    internal const string INVALID_LIST_ID = "Invalid list id.";
    internal const string LIST_NAME_REQUIRED = "List name is required and cannot be only whitespace characters.";
    internal const string LIST_NAME_LENGTH = "List name cannot exceed 255 characters.";
    internal const string TODO_DESC_REQUIRED = "Todo description is required and cannot be only whitespace characters.";
    internal const string TODO_DESC_LENGTH = "Todo description cannot exceed 255 characters.";
}

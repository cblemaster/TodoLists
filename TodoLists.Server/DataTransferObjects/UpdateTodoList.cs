using System.Text;

namespace TodoLists.Server.DataTransferObjects;

public class UpdateTodoList
{
    public int TodoListId { get; set; }
    public string Name { get; set; } = string.Empty;

    public (bool IsValid, string ErrorMessage) Validate() {
        bool isValid = false;
        StringBuilder errorMessage = new();

        if (TodoListId < 1) { errorMessage.Append("Invalid todo list id"); }
        if (string.IsNullOrWhiteSpace(Name)) { errorMessage.Append("Name is required"); }
        if (Name.Length < 1 || Name.Length > 255) { errorMessage.Append("Name is required and must be 255 characters or fewer"); }
        else { isValid = true; }

        return (isValid, errorMessage.ToString());
    }
}

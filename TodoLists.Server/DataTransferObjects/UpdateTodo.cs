using System.Text;

namespace TodoLists.Server.DataTransferObjects;

public class UpdateTodo
{
    public int TodoId { get; set; }
    public int TodoListId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplete { get; set; }

    public (bool IsValid, string ErrorMessage) Validate() {
        bool isValid = false;
        StringBuilder errorMessage = new();

        if (TodoId < 1) { errorMessage.Append("Invalid todo id"); }
        if (string.IsNullOrWhiteSpace(Description)) { errorMessage.Append("Description is required"); }
        if (Description.Length < 1 || Description.Length > 255) { errorMessage.Append("Description is required and must be 255 characters or fewer"); }
        else { isValid = true; }

        return (isValid, errorMessage.ToString());
    }
}

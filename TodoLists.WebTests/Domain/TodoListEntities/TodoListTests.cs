
using TodoLists.Web.Domain.TodoEntities;
using TodoLists.Web.Domain.TodoListEntities;

namespace TodoLists.WebTests.Domain.TodoListEntities;

[TestClass()]
public class TodoListTests
{
    [TestMethod()]
    public void WhenConstructingThrowsArgumentExceptionIfTodolistIdEqualsZero()
    {
        // arrange
        int TodoListId = 0;
        string Name = "test list";
        Todo[] Todos = [];

        // act & assert
        Assert.ThrowsException<ArgumentException>(() => new TodoList(TodoListId, Name, Todos));
    }
    [TestMethod()]
    public void WhenConstructingThrowsArgumentExceptionIfTodolistIdIsLessThanZero()
    {
        // arrange
        int TodoListId = -1;
        string Name = "test list";
        Todo[] Todos = [];

        // act & assert
        Assert.ThrowsException<ArgumentException>(() => new TodoList(TodoListId, Name, Todos));
    }
    [TestMethod()]
    public void WhenConstructingThrowsArgumentExceptionIfNameIsNull()
    {
        // arrange
        int TodoListId = 7;
        string Name = null!;
        Todo[] Todos = [];

        // act & assert
        Assert.ThrowsException<ArgumentException>(() => new TodoList(TodoListId, Name, Todos));
    }
    [TestMethod()]
    public void WhenConstructingThrowsArgumentExceptionIfNameIsEmptyString()
    {
        // arrange
        int TodoListId = 7;
        string Name = string.Empty;
        Todo[] Todos = [];

        // act & assert
        Assert.ThrowsException<ArgumentException>(() => new TodoList(TodoListId, Name, Todos));
    }
    [TestMethod()]
    public void WhenConstructingThrowsArgumentExceptionIfNameIsWhitespace()
    {
        // arrange
        int TodoListId = 7;
        string Name = "      ";
        Todo[] Todos = [];

        // act & assert
        Assert.ThrowsException<ArgumentException>(() => new TodoList(TodoListId, Name, Todos));
    }
    [TestMethod()]
    public void WhenConstructingThrowsArgumentExceptionIfNameIsMoreThan255Characters()
    {
        // arrange
        int TodoListId = 7;
        string Name = "null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!null!";
        Todo[] Todos = [];

        // act & assert
        Assert.ThrowsException<ArgumentException>(() => new TodoList(TodoListId, Name, Todos));
    }
    [TestMethod()]
    public void WhenConstructingSetsTodosToEmptyCollectionIfNull()
    {
        // arrange
        int TodoListId = 7;
        string Name = "test list";
        Todo[] Todos = null!;

        // act
        TodoList test = new(TodoListId, Name, Todos);

        // assert
        Assert.IsInstanceOfType<ICollection<Todo>>(test.Todos);
        Assert.IsTrue(test.Todos.Count == 0);

    }
    [TestMethod()]
    public void WhenConstructingCreatesObjectIfInputsAreValidAndTodosIsEmpty()
    {
        // arrange
        int TodoListId = 7;
        string Name = "test list";
        Todo[] Todos = [];

        // act
        TodoList test = new(TodoListId, Name, Todos);

        // assert
        Assert.IsFalse(test.TodoListId == 0);
        Assert.IsFalse(test.TodoListId < 0);
        Assert.IsFalse(string.IsNullOrWhiteSpace(test.Name));
        Assert.IsFalse(test.Name.Length > 255);
        Assert.IsInstanceOfType<ICollection<Todo>>(test.Todos);
        Assert.IsTrue(test.Todos.Count == 0);
    }
    [TestMethod()]
    public void WhenConstructingCreatesObjectIfInputsAreValidAndTodosIsSingle()
    {
        // arrange
        int TodoListId = 7;
        string Name = "test list";

        // act
        TodoList test = new(TodoListId, Name, [new Todo(1, TodoListId, "test todo", null, false, false, null!)]);

        // assert
        Assert.IsFalse(test.TodoListId == 0);
        Assert.IsFalse(test.TodoListId < 0);
        Assert.IsFalse(string.IsNullOrWhiteSpace(test.Name));
        Assert.IsFalse(test.Name.Length > 255);
        Assert.IsInstanceOfType<ICollection<Todo>>(test.Todos);
        Assert.IsTrue(test.Todos.Count == 1);
        Assert.IsInstanceOfType<Todo>(test.Todos.ToList()[0]);
    }
    [TestMethod()]
    public void WhenConstructingCreatesObjectIfInputsAreValidAndTodosIsMoreThanOne()
    {
        // arrange
        int TodoListId = 7;
        string Name = "test list";

        // act
        TodoList test = new(TodoListId, Name, [new Todo(1, TodoListId, "test todo 1", null, false, true, null!), new Todo(2, TodoListId, "test todo 2", null, true, false, null!)]);

        // assert
        Assert.IsFalse(test.TodoListId == 0);
        Assert.IsFalse(test.TodoListId < 0);
        Assert.IsFalse(string.IsNullOrWhiteSpace(test.Name));
        Assert.IsFalse(test.Name.Length > 255);
        Assert.IsInstanceOfType<ICollection<Todo>>(test.Todos);
        Assert.IsTrue(test.Todos.Count > 1);
        Assert.IsInstanceOfType<Todo>(test.Todos.ToList()[1]);
    }
}

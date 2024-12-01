
namespace TodoLists.API.Validation.Tests;

// TODO >> Code duplication here indicates that the class being tested needs refactoring, possibly with generics?

[TestClass()]
public class StringValidationExtensionsTests
{
    [TestMethod()]
    public void ValidateTodoListName_ReturnsIsNotValidWithErrorWhenInputIsWhitespace()
    {
        // arrange
        string input = "     ";

        // act
        ValidationResult sut = input.ValidateTodoListName();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoListName_ReturnsIsNotValidWithErrorWhenInputIsEmpty()
    {
        // arrange
        string input = string.Empty;

        // act
        ValidationResult sut = input.ValidateTodoListName();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoListName_ReturnsIsNotValidWithErrorWhenInputIsNull()
    {
        // arrange
        string input = null!;

        // act
        ValidationResult sut = input.ValidateTodoListName();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoListName_ReturnsIsNotValidWithErrorWhenInputIs300Chars()
    {
        // arrange
        string input = new('x', 300);

        // act
        ValidationResult sut = input.ValidateTodoListName();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoListName_ReturnsIsValidWithNoErrorsWhenInputIsValid()
    {
        // arrange
        string input = "test list name";

        // act
        ValidationResult sut = input.ValidateTodoListName();

        // assert
        Assert.IsTrue(sut.IsValid);
        Assert.IsFalse(sut.Errors.Any());
    }
    [TestMethod()]
    public void ValidateTodoDescription_ReturnsIsNotValidWithErrorWhenInputIsWhitespace()
    {
        // arrange
        string input = "     ";

        // act
        ValidationResult sut = input.ValidateTodoDescription();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoDescription_ReturnsIsNotValidWithErrorWhenInputIsEmpty()
    {
        // arrange
        string input = string.Empty;

        // act
        ValidationResult sut = input.ValidateTodoDescription();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoDescription_ReturnsIsNotValidWithErrorWhenInputIsNull()
    {
        // arrange
        string input = null!;

        // act
        ValidationResult sut = input.ValidateTodoDescription();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoDescription_ReturnsIsNotValidWithErrorWhenInputIs300Chars()
    {
        // arrange
        string input = new('x', 300);

        // act
        ValidationResult sut = input.ValidateTodoDescription();

        // assert
        Assert.IsFalse(sut.IsValid);
        Assert.IsTrue(sut.Errors.Any());
        CollectionAssert.AllItemsAreInstancesOfType(sut.Errors.ToArray(), typeof(string));
    }
    [TestMethod()]
    public void ValidateTodoDescription_ReturnsIsValidWithNoErrorsWhenInputIsValid()
    {
        // arrange
        string input = "test list name";

        // act
        ValidationResult sut = input.ValidateTodoDescription();

        // assert
        Assert.IsTrue(sut.IsValid);
        Assert.IsFalse(sut.Errors.Any());
    }
}

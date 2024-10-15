USE master
GO

DECLARE @sql nvarchar(1000);

IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'TodoLists')

BEGIN
    SET @sql = N'USE TodoLists;

                 ALTER DATABASE TodoLists SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                 USE master;

                 DROP DATABASE TodoLists;';
    EXEC (@sql);
END;

CREATE DATABASE TodoLists
GO

USE TodoLists
GO

CREATE TABLE TodoList (
	TodoListId INT IDENTITY(1,1) NOT NULL,
	[Name] VARCHAR(255) NOT NULL,
	CONSTRAINT PK_TodoList PRIMARY KEY(TodoListId),
)
GO

CREATE TABLE Todo (
	TodoId INT IDENTITY(1,1) NOT NULL,
	TodoListId INT NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	DueDate DATE NULL,
	IsImportant BIT NOT NULL,
	IsComplete BIT NOT NULL,
	CONSTRAINT PK_Todo PRIMARY KEY(TodoId),
	CONSTRAINT FK_Todo_TodoList FOREIGN KEY(TodoListId) REFERENCES TodoList(TodoListId),
)
GO

-- optional sample data
--INSERT INTO TodoList([Name]) VALUES('List One');
--INSERT INTO TodoList([Name]) VALUES('List Two');
--INSERT INTO TodoList([Name]) VALUES('List Three');
--INSERT INTO TodoList([Name]) VALUES('List A');
--INSERT INTO TodoList([Name]) VALUES('List B');
--INSERT INTO TodoList([Name]) VALUES('List C');
--INSERT INTO TodoList([Name]) VALUES('List 1');
--INSERT INTO TodoList([Name]) VALUES('List 2');
--INSERT INTO TodoList([Name]) VALUES('List 3');

--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List One'), 'djfjfj', DATEADD(day,1,GETDATE()), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List One'), 'dkdfkfkf', GETDATE(), 1, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List Two'), 'mkridc', DATEADD(day,14,GETDATE()), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List Two'), 'nastrp', DATEADD(day,3,GETDATE()), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List Three'), 'ayhgfik', GETDATE(), 0, 1);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List Three'), 'akgjoip', GETDATE(), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List Three'), 'zjiklho', DATEADD(day,5,GETDATE()), 1, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List A'), 'clkgklg', DATEADD(day,10,GETDATE()), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List B'), 'cmgjut', DATEADD(day,14,GETDATE()), 0, 1);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List B'), 'folhjphg', DATEADD(month,3,GETDATE()), 1, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List C'), 'djhgou', DATEADD(day,-30,GETDATE()), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List C'), 'qjntfht', GETDATE(), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List C'), 'vlkiaprfd', DATEADD(day,2,GETDATE()), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List C'), 'nmgy', GETDATE(), 1, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List C'), 'ckjgthjna', DATEADD(day,-10,GETDATE()), 1, 1);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List C'), 'nvghdshs', GETDATE(), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 1'), 'natryhjmb', GETDATE(), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 2'), 'nbhgvba', DATEADD(day,6,GETDATE()), 1, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 3'), 'jvjjjffza', DATEADD(day,7,GETDATE()), 0, 1);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 1'), 'ndivhfkg', GETDATE(), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 2'), 'pognjbivdknc', DATEADD(day,-8,GETDATE()), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 2'), 'dskvlnHsd', DATEADD(day,12,GETDATE()), 1, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 3'), 'sdihsDUiVBHi', GETDATE(), 0, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 3'), 'sdfj[EDGORF', GETDATE(), 1, 0);
--INSERT INTO Todo(TodoListId, [Description], DueDate, IsImportant, IsComplete) VALUES ((SELECT l.ToDoListId FROM TodoList l WHERE l.[Name] = 'List 3'), 'cdspfJsiogv', DATEADD(day,3,GETDATE()), 0, 1);

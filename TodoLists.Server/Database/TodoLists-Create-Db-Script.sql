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

---- optional sample data
--INSERT INTO TodoList([Name]) VALUES('List One');
--INSERT INTO TodoList([Name]) VALUES('List Two');
--INSERT INTO TodoList([Name]) VALUES('List Three');
--INSERT INTO TodoList([Name]) VALUES('List A');
--INSERT INTO TodoList([Name]) VALUES('List B');
--INSERT INTO TodoList([Name]) VALUES('List C');
--INSERT INTO TodoList([Name]) VALUES('List 1');
--INSERT INTO TodoList([Name]) VALUES('List 2');
--INSERT INTO TodoList([Name]) VALUES('List 3');

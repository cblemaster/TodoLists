
# TodoLists
## About
+ An application that provides todo functionality, nothing fancy, just the basics
## Objectives
+ Build an app that different UI projects can call
+ A jumping off point for learning new UI technologies
+ See if there is anything new in .NET 9 that I want to use
+ Learn EF Core code first with migrations
+ Use guids as the entity keys - what issues do I run into?
## Built with
+ Microsoft Visual Studio Community 2022
+ Visual Studio Code
+ NET 9 / C# 13
+ SQL server database
+ Entity Framework Core 9 (code first with migrations)
+ ASP.NET Core (minimal apis)
## Features / Use cases
+ See list of all todo lists, sorted by name
+ Select a todo list to see it's details and it's todos
+ Create todo list
+ Rename todo list
+ Delete todo list
+ See list of todos for a given list, sorted by due date desc then name
+ Create todo
+ Update todo
+ Delete todo
+ Virtual lists:
++ See todos due today
++ See important todos
++ See completed todos
## Business / validation rules
+ (Validation) Todo list name is required and must be 255 chars or fewer
+ (Validation) Todo description is required and must be 255 chars or fewer
+ Completed todos cannot be updated, except to flip them back to not complete
+ Due date for todo is optional and can be in the past
+ Important todos cannot be deleted
+ Todo lists cannot be deleted if they have any todo 'children' that are not complete; todo lists having only completed todos can be deleted
## Instructions for running the app (TBD)
## Improvement opportunities (TBD)
+ See todo comments in code

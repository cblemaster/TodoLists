
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

//  TODO >> bring validation & BR into this! It's a domain model!

//  + get all todo lists, do not include todos
//  + get todo list by id, including todos
//	+ get all todos where DueDate == today && IsComplete == false;
//	+ get all todos where IsImportant == true && IsComplete == false;
//  + get all todos where IsComplete == true;

//	+ (post) create todo list
//	+ (post) create todo
//  + (post) move todo to new list

//	+ (put) rename todo list
//	+ (put) mark complete/incomplete
//	+ (put) change due date
//	+ (put) toggle importance
//	+ (put) change description
//	+ (put) move to list
//	+ (put) move to list

//  + delete todo - only if IsImportant == false;
//	+ delete todo list - only if ToDos.All(t => t.IsComplete)

import { DatePipe } from "@angular/common";

export class Todo {
    constructor(private datePipe : DatePipe) {}
	
	todoId : number = 0;
	todoListId : number = 0;
	description : string = '';
	dueDate : Date = new Date();
	isImportant : boolean = false;
	isComplete : boolean = false;
}

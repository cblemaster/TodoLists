import { Todo } from "./todo";

export class TodoList {
    public todoListId : number | undefined;
    public name : string | undefined;
    public todos : Todo[] = [];
}

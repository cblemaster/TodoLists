import { Todo } from "./todo";
export interface TodoList {
  todoListId: number;
  name: string;
  todos: Todo[];
}

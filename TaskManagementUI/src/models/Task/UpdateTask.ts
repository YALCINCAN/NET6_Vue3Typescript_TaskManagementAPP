export interface UpdateTask {
  id:number;
  title: string;
  description: string;
  stringdeadline: string;
  taskStatusId:number;
  userIds:string[]
}

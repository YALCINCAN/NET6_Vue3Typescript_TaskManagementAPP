import { CommentDTO } from '../Comment/CommentDTO';
import { UserTaskDTO } from './UserTaskDTO';
export interface TaskDetailDTO{
  id:number;
  creatorId:string;
  title:string;
  description:string;
  taskStatusId:number;
  deadline:string;
  assignedUsers:UserTaskDTO[];
  comments:CommentDTO[];
}

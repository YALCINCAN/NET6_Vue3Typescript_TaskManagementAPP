import { UserDTO } from '../UserDTO';
export interface CommentDTO{
  id:number;
  commentDate:Date;
  description:string;
  userId:string;
  taskId:number;
  user:UserDTO;
}

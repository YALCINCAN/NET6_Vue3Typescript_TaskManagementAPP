import { UserDTO } from '../UserDTO';
export interface UserTaskDTO {
  taskId:number;
  userId:string;
  user:UserDTO
}

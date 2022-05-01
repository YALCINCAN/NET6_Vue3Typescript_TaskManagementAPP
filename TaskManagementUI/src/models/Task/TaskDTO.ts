import { UserTaskDTO } from './UserTaskDTO';

export interface TaskDTO {
  id: number;
  title: string;
  deadline: string;
  creatorId: string;
  status: string;
  assignedUsers: UserTaskDTO[];
}

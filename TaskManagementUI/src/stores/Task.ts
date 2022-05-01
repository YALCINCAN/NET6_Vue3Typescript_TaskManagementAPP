import { UpdateTask } from './../models/Task/UpdateTask';
import { CreateTask } from './../models/Task/CreateTask';
import { TaskDetailDTO } from './../models/Task/TaskDetailDTO';
import { TaskDTO } from '../models/Task/TaskDTO';
import { DataResponse } from './../models/Responses/DataResponse';
import { useAlertStore } from './Alert';
import { ErrorResponse } from './../models/Responses/ErrorResponse';
import { api } from 'boot/axios';
import { defineStore } from 'pinia';
import { AxiosError } from 'axios';
import { SuccessResponse } from 'src/models/Responses/SuccessResponse';

interface TaskStore {
  tasks: TaskDTO[];
  filteredtasks: TaskDTO[];
  taskdetail: TaskDetailDTO;
  showDialog: boolean;
  showTaskDetailDialog: boolean;
  selectedtaskId: number;
}

export const useTaskStore = defineStore({
  id: 'task',
  state: (): TaskStore => ({
    tasks: [],
    filteredtasks: [],
    taskdetail: { description: '' } as TaskDetailDTO,
    showDialog: false,
    selectedtaskId: 0,
    showTaskDetailDialog: false,
  }),
  getters: {
    getTasksFromState(): TaskDTO[] {
      return this.tasks;
    },
    getTaskDetailFromState(): TaskDetailDTO {
      return this.taskdetail;
    },
    getShowDialog(): boolean {
      return this.showDialog;
    },
    getFilteredTasks(): TaskDTO[] {
      return this.filteredtasks;
    },
  },
  actions: {
    async getTasks() {
      await api.get<DataResponse<TaskDTO[]>>('tasks').then((response) => {
        if (response.data.success) {
          this.tasks = response.data.data;
          this.filteredtasks = response.data.data;
        }
      });
    },
    async getTaskDetail(taskId: number) {
      this.selectedtaskId = taskId;
      this.showTaskDetailDialog = true;
      await api
        .get<DataResponse<TaskDetailDTO>>(`tasks/${taskId}`)
        .then((response) => {
          if (response.data.success) {
            this.taskdetail = response.data.data;
          }
        });
    },
    async createTask(task: CreateTask) {
      return await api
        .post<SuccessResponse>('tasks', task)
        .then(async (response) => {
          if (response.data.success) {
            await this.getTasks();
            return response.data.message;
          }
        })
        .catch((error: AxiosError) => {
          if (error.response && error.response.data) {
            const errorresponse = error.response.data as ErrorResponse;
            const alertStore = useAlertStore();
            alertStore.clearAlert();
            alertStore.setErrors(errorresponse.errors);
          }
        });
    },
    async updateTask(task: UpdateTask) {
      return await api
        .put<SuccessResponse>('tasks', task)
        .then(async (response) => {
          if (response.data.success) {
            this.tasks = [];
            this.filteredtasks=[];
            await this.getTasks();
            return response.data.message;
          }
        })
        .catch((error: AxiosError) => {
          if (error.response && error.response.data) {
            const errorresponse = error.response.data as ErrorResponse;
            const alertStore = useAlertStore();
            alertStore.setErrors(errorresponse.errors);
          }
        });
    },
    async removeTask(taskId: number) {
      return await api
        .delete<SuccessResponse>(`tasks/${taskId}`)
        .then(async (response) => {
          if (response.data.success) {
            this.tasks = [];
            this.filteredtasks=[];
            await this.getTasks();
            return response.data.message;
          }
        })
        .catch((error: AxiosError) => {
          if (error.response && error.response.data) {
            const errorresponse = error.response.data as ErrorResponse;
            const alertStore = useAlertStore();
            alertStore.setErrors(errorresponse.errors);
          }
        });
    },
    showCreateTaskDialog() {
      this.showDialog = true;
    },
    clearSelectedTaskId(): void {
      this.selectedtaskId = 0;
      this.showTaskDetailDialog = false;
      this.showDialog = false;
      this.taskdetail = {} as TaskDetailDTO;
    },
    searchTask(searchtext: string) {
      searchtext != '' && searchtext != null
        ? (this.filteredtasks = this.tasks.filter((task) => {
            return task.title.toLowerCase().includes(searchtext.toLowerCase());
          }))
        : this.tasks;
    },
    filterTasks(filter: string) {
      if (filter == 'all') {
        this.filteredtasks = this.tasks;
      } else {
        this.filteredtasks = this.tasks.filter((task) => {
          return task.status.toLowerCase() == filter.toLowerCase();
        });
      }
    },
  },
});

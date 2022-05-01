import { defineStore } from "pinia";
import { DataResponse } from "src/models/Responses/DataResponse";
import { api } from "src/boot/axios";
import { TaskStatusDTO } from "src/models/TaskStatus/TaskStatusDTO";

interface TaskStatusStore {
  taskStatuses: TaskStatusDTO[];
}

export const useTaskStatusStore = defineStore({
  id: "taskStatus",
  state: (): TaskStatusStore => ({
    taskStatuses: [],
  }),
  getters: {
    getTaskStatusesFromState(): TaskStatusDTO[] {
      return this.taskStatuses;
    }
  },
  actions: {
    async getTaskStatuses() {
      await api.get<DataResponse<TaskStatusDTO[]>>('taskstatuses').then((response) => {
        if (response.data.success) {
          this.taskStatuses = response.data.data;
        }
      });
    }
  }
});

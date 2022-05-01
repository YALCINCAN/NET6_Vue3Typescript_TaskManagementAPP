import { useTaskStore } from './Task';
import { UpdateComment } from './../models/Comment/UpdateComment';
import { CreateComment } from './../models/Comment/CreateComment';
import { ErrorResponse } from './../models/Responses/ErrorResponse';
import { api } from 'boot/axios';
import { defineStore } from 'pinia';
import { AxiosError } from 'axios';
import { SuccessResponse } from './../models/Responses/SuccessResponse';
import { useAlertStore } from './Alert';

export const useCommentStore = defineStore({
  id: 'comment',
  state: () => ({}),
  actions: {
    async addComment(comment: CreateComment) {
      return await api
        .post<SuccessResponse>('comments', comment)
        .then(async (response) => {
          if (response.data.success) {
            const taskStore = useTaskStore();
            await taskStore.getTaskDetail(taskStore.selectedtaskId);
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
    async updateComment(comment: UpdateComment) {
      return await api
        .put<SuccessResponse>('comments', comment)
        .then(async (response) => {
          if (response.data.success) {
            const taskStore = useTaskStore();
            await taskStore.getTaskDetail(taskStore.selectedtaskId);
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
    async removeComment(commentId: number) {
      return await api
        .delete<SuccessResponse>(`comments/${commentId}`)
        .then(async (response) => {
          if (response.data.success) {
            const taskStore = useTaskStore();
            await taskStore.getTaskDetail(taskStore.selectedtaskId);
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
    }
  },
});

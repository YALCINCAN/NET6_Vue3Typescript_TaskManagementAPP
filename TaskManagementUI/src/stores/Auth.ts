import { ConfirmEmail } from '../models/Auth/ConfirmEmail';
import { DataResponse } from './../models/Responses/DataResponse';
import { Register } from '../models/Auth/Register';
import { useAlertStore } from './Alert';
import { ErrorResponse } from './../models/Responses/ErrorResponse';
import { TokenDTO } from '../models/Auth/TokenDTO';
import { Login } from '../models/Auth/Login';
import { UserDTO } from '../models/UserDTO';
import { api } from 'boot/axios';
import { defineStore } from 'pinia';
import { AxiosError } from 'axios';
import { SuccessResponse } from 'src/models/Responses/SuccessResponse';

interface AuthStore {
  user: UserDTO;
  loggedIn: boolean;
  searcheduser: UserDTO;
}

export const useAuthStore = defineStore({
  id: 'auth',
  state: (): AuthStore => ({
    user: {} as UserDTO,
    loggedIn: false,
    searcheduser: {} as UserDTO,
  }),
  getters: {
    getLoggedIn(): boolean {
      return this.loggedIn;
    },
    getUser(): UserDTO {
      return this.user;
    },
    getSearchedUserFromState(): UserDTO {
      return this.searcheduser;
    },
  },
  actions: {
    async login(logincredentials: Login) {
      return await api
        .post<DataResponse<TokenDTO>>('auth/login', logincredentials)
        .then(async (response) => {
          if (response.data.success) {
            localStorage.setItem('accessToken', response.data.data.accessToken);
            await this.getAuthenticatedUser();
            return response.data.success;
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
    logout() {
      localStorage.removeItem('accessToken');
      this.user = {} as UserDTO;
      this.loggedIn = false;
    },
    async register(registercredentials: Register) {
      await api
        .post<SuccessResponse>('auth/register', registercredentials)
        .then((response) => {
          if (response.data.success) {
            const alertStore = useAlertStore();
            alertStore.setMessage(response.data.message);
          }
        })
        .catch((error: AxiosError) => {
          if (error.response && error.response.data) {
            const alertStore = useAlertStore();
            alertStore.clearAlert();
            const errorresponse = error.response.data as ErrorResponse;
            alertStore.setErrors(errorresponse.errors);
          }
        });
    },
    async getAuthenticatedUser() {
      await api.get<DataResponse<UserDTO>>('auth').then((response) => {
        this.user = response.data.data;
        this.loggedIn = true;
      });
    },
    async getUserByEmail(email: string) {
      await api
        .get<DataResponse<UserDTO>>(`auth/${email}`)
        .then((response) => {
          if (response.data.success) {
            this.searcheduser = response.data.data;
          }
        })
        .catch((error: AxiosError) => {
          if (error.response && error.response.data) {
            const alertStore = useAlertStore();
            alertStore.clearAlert();
            const errorresponse = error.response.data as ErrorResponse;
            alertStore.setErrors(errorresponse.errors);
          }
        });
    },
    async confirmEmail(confirmemaildata: ConfirmEmail) {
      await api
        .post<SuccessResponse>('auth/confirmemail', confirmemaildata)
        .then((response) => {
          const alertStore = useAlertStore();
          alertStore.setMessage(response.data.message);
        })
        .catch((error: AxiosError) => {
          if (error.response && error.response.data) {
            const errorresponse = error.response.data as ErrorResponse;
            const alertStore = useAlertStore();
            alertStore.setErrors(errorresponse.errors);
          }
        });
    },
  },
});

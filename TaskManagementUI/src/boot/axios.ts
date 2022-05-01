import { boot } from 'quasar/wrappers';
import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $axios: AxiosInstance;
  }
}
const baseUrl = 'http://localhost:5129/api/';
// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({ baseURL: baseUrl });


export default boot(({ app, redirect }) => {
  api.interceptors.request.use(
    (config: AxiosRequestConfig): AxiosRequestConfig => {
      const accessToken = localStorage.getItem('accessToken');
      if (accessToken) config.headers.Authorization = `Bearer ${accessToken}`;
      return config;
    }
  );
  api.interceptors.response.use(
    (config) => config,
    async (error) => {
      if (
        error.response &&
        error.response.status === 401
      ) {
          redirect('/auth');
      } else throw error;
    }
  );
});

export { api };

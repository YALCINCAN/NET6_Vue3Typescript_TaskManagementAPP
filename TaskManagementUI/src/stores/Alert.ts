import { defineStore } from 'pinia';

interface AlertStore {
  errors: string[];
  message: string;
  show: boolean;
}

export const useAlertStore = defineStore({
  id: 'alert',
  state: (): AlertStore => ({
    errors: [],
    message: '',
    show: false,
  }),
  getters: {
    getMessage(): string {
      return this.message;
    },
    getErrors(): string[] {
      return this.errors;
    },
    getShowStatus(): boolean {
      return this.show;
    },
  },
  actions: {
    setMessage(message: string) {
      if (message) {
        this.message = message;
        this.show = true;
      }
    },
    setErrors(errors: string[]) {
      if (errors) {
        this.errors = errors;
        this.show = true;
      }
    },
    clearAlert() {
      this.errors = [];
      (this.message = ''), (this.show = false);
    },
  },
});

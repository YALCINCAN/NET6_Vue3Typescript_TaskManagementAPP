import { useQuasar } from 'quasar';

export default function useNotify() {
  const $q = useQuasar();

  const notifySuccess = (message: string) => {
    $q.notify({
      type: 'positive',
      message: message || 'All right !',
      timeout: 2000,
      position: 'top-right',
    });
  };

  return {
    notifySuccess,
  };
}

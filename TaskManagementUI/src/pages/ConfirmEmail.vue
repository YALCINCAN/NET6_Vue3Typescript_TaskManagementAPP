<template>
  <div class="tw-max-w-7xl tw-mx-auto tw-px-3 tw-py-3">
    <Alert></Alert>
    <div class="tw-flex tw-justify-center tw-items-center">
      <q-btn to="/auth" color="secondary">Login</q-btn>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
import { reactive, onMounted } from 'vue';
import { ConfirmEmail } from 'src/models/Auth/ConfirmEmail';
import { useAuthStore } from 'src/stores/Auth';
import Alert from 'src/components/Alert.vue';
const route = useRoute();
const authStore = useAuthStore();
const confirmemaildata = reactive<ConfirmEmail>({
  userId: route.params.userId as string,
  token: route.params.token as string,
});
async function confirmEmail() {
  await authStore.confirmEmail(confirmemaildata);
}
onMounted(async () => {
  await confirmEmail();
});
</script>

<style scoped></style>

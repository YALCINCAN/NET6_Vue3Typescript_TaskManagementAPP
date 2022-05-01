<template>
  <router-view />
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useAuthStore } from 'src/stores/Auth';
const authStore = useAuthStore();
onMounted(async () => {
  let accesstoken = localStorage.getItem('accessToken');
  if (accesstoken) {
    await authStore.getAuthenticatedUser();
  }
});
</script>

<template>
  <div v-if="show">
    <q-banner class="text-white q-mb-md" v-if="show" :class="success">
      <div class="row items-center">
        <div class="col-2">
          <i :class="isError"></i>
        </div>
        <div class="col-10">
          <div class="q-ma-xs" v-if="message != ''">
            {{ message }}
          </div>
          <div class="q-ma-xs" v-else>
            <div v-for="(error, index) in errors" :key="index">
              {{ error }}
            </div>
          </div>
        </div>
      </div>
    </q-banner>
  </div>
</template>

<script setup lang='ts'>
import { watch, onMounted,computed } from 'vue';
import { useRoute } from 'vue-router';
import { useAlertStore } from 'src/stores/Alert';

const route = useRoute();
const alertStore = useAlertStore();
const success = computed(() => {
  return message.value != '' ? 'tw-bg-green-500' : 'tw-bg-red-500';
});
const isError = computed(() => {
  return errors.value.length > 0
    ? 'fas fa-exclamation-circle fa-2x'
    : 'fas fa-check-circle fa-2x';
});
const message = computed<string>(() => {
  return alertStore.getMessage;
});

const show = computed<boolean>(() => {
  return alertStore.getShowStatus;
});

const errors = computed<string[]>(() => {
  return alertStore.getErrors;
});
onMounted(() => {
  alertStore.clearAlert();
});
watch(
  () => route.path,
  () => {
    alertStore.clearAlert();
  }
);
</script>

<style lang="scss" scoped></style>

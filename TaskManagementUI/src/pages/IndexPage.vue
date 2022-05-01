<template>
  <div class="tw-max-w-7xl tw-mx-auto tw-mt-8 tw-px-3 lg:tw-px-0 tw-mb-8">
    <div
      class="tw-grid tw-grid-cols-1 sm:tw-grid-cols-2 md:tw-grid-cols-3 lg:tw-grid-cols-5 tw-gap-5 tw-place-items-center"
    >
      <task v-for="task in tasks" :key="task.id" :task="task"></task>
    </div>
    <create-task-vue></create-task-vue>
    <task-detail-vue></task-detail-vue>
  </div>
</template>

<script lang="ts" setup>
import Task from 'src/components/Task.vue';
import { TaskDTO } from 'src/models/Task/TaskDTO';
import { useTaskStore } from 'src/stores/Task';
import { computed,onMounted} from 'vue';
import CreateTaskVue from 'src/components/CreateTask.vue';
import TaskDetailVue from 'src/components/TaskDetail.vue';

const taskStore = useTaskStore();

onMounted(async () => {
  await taskStore.getTasks();
})

const tasks = computed<TaskDTO[]>(() => {
  return taskStore.getFilteredTasks
});



</script>

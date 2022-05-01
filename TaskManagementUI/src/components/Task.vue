<template>
  <q-card
    @click="taskStore.getTaskDetail(task.id)"
    v-if="task"
    class="tw-bg-gray-500 tw-text-white tw-h-full tw-w-full hover:tw-bg-gray-900 hover:tw-cursor-pointer"
  >
    <div class="tw-flex tw-flex-col tw-text-white">
      <div
        class="tw-flex tw-flex-col tw-text-center tw-items-center tw-py-5 tw-space-y-4"
      >
        <p class="tw-leading-loose tw-break-all">
          {{ task.title }}
        </p>
        <hr class="tw-border tw-border-white tw-w-full" />
        <div>Assigned Users</div>
        <div
          class="tw-flex tw-justify-center tw-items-center tw-flex-wrap tw-space-x-2"
        >
          <q-badge
            v-for="assigneduser in task.assignedUsers"
            :key="assigneduser.user.id"
            class="tw-my-2"
            rounded
            color="purple"
            :label="assigneduser.user.firstName"
          />
        </div>
        <hr class="tw-border tw-border-white tw-w-full" />
        <div>Deadline : {{ task.deadline }}</div>
        <div class="tw-flex tw-items-center">
          <div>
            <q-badge
              v-if="task.status == 'Ongoing'"
              color="yellow"
              rounded
              class="q-mr-sm"
            />
            <q-badge v-else color="green" rounded class="q-mr-sm" />
          </div>
          <div>
            {{ task.status }}
          </div>
        </div>
      </div>
    </div>
  </q-card>
</template>

<script setup lang="ts">
import { defineProps, PropType, ref } from 'vue';
import { TaskDTO } from 'src/models/Task/TaskDTO';
import { useTaskStore } from 'src/stores/Task';
const props = defineProps({
  task: Object as PropType<TaskDTO>,
});
const taskStore = useTaskStore();
const task = ref<TaskDTO>(props.task as TaskDTO);
</script>

<style scoped></style>

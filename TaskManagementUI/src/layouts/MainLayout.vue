<template>
  <q-layout view="hhh lpR fFf">
    <q-header class="bg-purple">
      <div
        class="tw-max-w-7xl tw-h-24 tw-mx-auto tw-flex tw-items-center lg:tw-px-0 tw-px-5 tw-space-x-2"
        :class="loggedIn ? 'tw-justify-between' : 'tw-justify-center'"
      >
        <div>
          <q-toolbar-title class="tw-text-xs md:tw-text-base"> Task Management </q-toolbar-title>
        </div>
        <div>
          <q-btn
            v-if="loggedIn"
            color="primary"
            label="Create Task"
            icon="add"
            size="sm"
            class="tw-hidden md:tw-block"
            @click="taskStore.showCreateTaskDialog()"
          ></q-btn>
        </div>
        <div class="tw-flex tw-items-center tw-space-x-2" v-if="loggedIn">
          <q-input
            :input-style="{ color: '#9F2EB2'}"
            color="purple"
            label-color="purple"
            bg-color="white"
            label="Search"
            v-model="search"
            filled
            type="search"
          >
          </q-input>
          <q-icon size="md" name="filter_list">
            <q-menu>
              <q-list style="min-width: 100px">
                <q-item clickable v-close-popup>
                  <q-item-section @click="filterTasks('all')">All Tasks</q-item-section>
                </q-item>
                <q-item clickable v-close-popup>
                  <q-item-section @click="filterTasks('Completed')">Completed Tasks</q-item-section>
                </q-item>
                <q-item clickable v-close-popup>
                  <q-item-section @click="filterTasks('Ongoing')">Ongoing Tasks</q-item-section>
                </q-item>
              </q-list>
            </q-menu>
          </q-icon>
        </div>
        <div class="tw-flex tw-items-center tw-space-x-4">
          <div class="tw-text-base tw-hidden md:tw-flex" v-if="loggedIn">
            {{ authenticateduser.firstName }} {{ authenticateduser.lastName }}
          </div>
          <div v-if="loggedIn">
            <q-icon @click="logout()" size="md" name="logout"></q-icon>
          </div>
        </div>
      </div>
    </q-header>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue';
import { useAuthStore } from 'src/stores/Auth';
import { useTaskStore } from 'src/stores/Task';
import { useRouter } from 'vue-router';

const router = useRouter();
const taskStore = useTaskStore();
const authStore = useAuthStore();
const loggedIn = computed(() => {
  return authStore.getLoggedIn;
});

const authenticateduser = computed(() => {
  return authStore.getUser;
});

function logout() {
  authStore.logout();
  router.push('/auth');
}

function filterTasks(filter:string){
  taskStore.filterTasks(filter);
}
const search = ref('');

watch(
  () => search.value,
  (newValue) => {
    if (search.value == '') {
      taskStore.filteredtasks = taskStore.tasks;
    }
    taskStore.searchTask(newValue);
  }
);
</script>

<style scoped></style>

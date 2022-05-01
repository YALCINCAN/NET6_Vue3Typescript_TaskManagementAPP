<template>
  <q-dialog v-model="showDialog" persistent>
    <q-card style="width: 600px; max-width: 80vw">
      <q-card-section>
        <Alert></Alert>
        <div class="text-h6">
          <span>Create Task</span>
          <q-btn
            round
            flat
            dense
            icon="close"
            class="float-right"
            color="grey-8"
            v-close-popup
            @click="resetCreateTask()"
          ></q-btn>
        </div>
      </q-card-section>
      <q-separator inset></q-separator>
      <q-card-section class="q-pt-none">
        <q-form class="q-gutter-md">
          <q-list>
            <q-item>
              <q-item-section>
                <q-input
                  dense
                  type="text"
                  outlined
                  label="Title"
                  v-model="task.title"
                  :error="$v.title.$error"
                >
                  <template #error>
                    <li v-for="(error, index) in $v.title.$errors" :key="index">
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-editor
                  v-model="task.description"
                  min-height="15rem"
                  placeholder="Description"
                >
                </q-editor>
                <span v-if="$v.description.$error" class="text-red"
                  >Description is required</span
                >
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-input
                  filled
                  v-model="date"
                  :error="$v.stringdeadline.$error"
                >
                  <template v-slot:prepend>
                    <q-icon name="event" class="cursor-pointer">
                      <q-popup-proxy
                        cover
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-date v-model="date" mask="DD-MM-YYYY HH:mm">
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Close"
                              color="primary"
                              flat
                            />
                          </div>
                        </q-date>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                  <template v-slot:append>
                    <q-icon name="access_time" class="cursor-pointer">
                      <q-popup-proxy
                        cover
                        transition-show="scale"
                        transition-hide="scale"
                      >
                        <q-time
                          v-model="date"
                          mask="DD-MM-YYYY HH:mm"
                          format24h
                        >
                          <div class="row items-center justify-end">
                            <q-btn
                              v-close-popup
                              label="Close"
                              color="primary"
                              flat
                            />
                          </div>
                        </q-time>
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                  <template #error>
                    <li
                      v-for="(error, index) in $v.stringdeadline.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
              </q-item-section>
            </q-item>
            <div class="tw-flex tw-items-center tw-justify-center tw-space-x-2">
              <q-item>
                <q-item-section>
                  <div class="tw-text-center tw-my-2">Assigned Users</div>
                  <span v-if="!assignedusers.length > 0" class="text-red">
                    <div class="tw-flex tw-items-center tw-justify-center">
                      <span>Atleast one user is required</span>
                    </div>
                  </span>
                  <div
                    class="tw-flex tw-items-center tw-justify-center tw-space-x-2"
                  >
                    <q-badge
                      class="tw-flex tw-items-center tw-justify-center"
                      v-for="assigneduser in assignedusers"
                      :key="assigneduser.id"
                      :label="assigneduser.firstName"
                      @click="removeUserFromAssignedUsers(assigneduser.id)"
                    >
                    </q-badge>
                  </div>
                </q-item-section>
              </q-item>
            </div>
            <q-item>
              <q-item-section>
                <div class="tw-flex tw-justify-center tw-items-center">
                  <q-btn @click="assignMe" label="Assign Me"></q-btn>
                </div>
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-input
                  class="tw-my-2"
                  dense
                  outlined
                  placeholder="Search user by email"
                  v-model="email"
                ></q-input>
                <q-btn
                  @click="searchUserByEmail(email)"
                  label="Assign User By Email"
                ></q-btn>
              </q-item-section>
            </q-item>
          </q-list>
        </q-form>
      </q-card-section>
      <q-card-actions align="right" class="text-teal">
        <q-btn
          :disable="$v.$invalid || !assignedusers.length > 0"
          @click="createTask()"
          label="Create"
          color="primary"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { useTaskStore } from 'src/stores/Task';
import Alert from './Alert.vue';
import { computed, ref, watch } from 'vue';
import { CreateTask } from 'src/models/Task/CreateTask';
import { UserDTO } from 'src/models/UserDTO';
import { useAuthStore } from 'src/stores/Auth';
import useNotify from 'src/composables/useNotify';
import useVuelidate from '@vuelidate/core';
import { required, helpers } from '@vuelidate/validators';

const email = ref<string>('');
const assignedusers = ref<UserDTO[]>([]);
const date = ref('');
const task = ref<CreateTask>({
  description: '',
  userIds: [] as string[],
} as CreateTask);

const { notifySuccess } = useNotify();
const taskStore = useTaskStore();
const authStore = useAuthStore();
const showDialog = computed<boolean>(() => {
  return taskStore.getShowDialog;
});

function assignMe() {
  var user = authStore.getUser;
  if (user && !assignedusers.value.some((x) => x.id === user.id)) {
    assignedusers.value.push(user);
  }
}

async function searchUserByEmail(email: string) {
  if (email) {
    await authStore.getUserByEmail(email);
    var searcheduser = authStore.getSearchedUserFromState;
    if (
      searcheduser.id &&
      !assignedusers.value.some((x) => x.id === searcheduser.id)
    ) {
      assignedusers.value.push(searcheduser);
    }
  }
}

async function createTask() {
  task.value.userIds = assignedusers.value.map((x) => x.id);
  const responsemessage = await taskStore.createTask(task.value);
  if (responsemessage) {
    resetCreateTask();
    notifySuccess(responsemessage);
  }
}

function removeUserFromAssignedUsers(id: string) {
  var existingIndex = assignedusers.value.findIndex((x) => x.id === id);
  if (existingIndex > -1) {
    assignedusers.value.splice(existingIndex, 1);
  }
}

function resetCreateTask() {
  taskStore.clearSelectedTaskId();
  email.value = '';
  task.value = {description:'',userIds:[] as string[]} as CreateTask;
  date.value=''
  assignedusers.value = [];
}

watch(
  () => date.value,
  (newValue) => {
    task.value.stringdeadline = newValue;
  }
);

const rules = computed(() => {
  return {
    title: {
      required: helpers.withMessage('Title is required', required),
    },
    description: {
      required: helpers.withMessage('Description is required', required),
    },
    stringdeadline: {
      required: helpers.withMessage('Deadline is required', required),
    },
  };
});

const $v = useVuelidate(rules, task, { $autoDirty: true });
</script>

<style scoped></style>

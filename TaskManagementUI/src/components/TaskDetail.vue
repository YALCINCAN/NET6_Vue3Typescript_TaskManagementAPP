<template>
  <q-scroll-area>
    <q-dialog v-model="taskStore.showTaskDetailDialog" persistent>
      <q-card style="width: 600px; max-width: 80vw">
        <q-card-section>
          <Alert></Alert>
          <div class="text-h6">
            <span>Task Detail</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetTaskDetail()"
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
                    :readonly="authStore.getUser.id != task.creatorId"
                  >
                    <template #error>
                      <li
                        v-for="(error, index) in $v.title.$errors"
                        :key="index"
                      >
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
                    :readonly="authStore.getUser.id != task.creatorId"
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
                    v-model="task.deadline"
                    :error="$v.deadline.$error"
                    :readonly="authStore.getUser.id != task.creatorId"
                  >
                    <template v-slot:prepend>
                      <q-icon name="event" class="cursor-pointer">
                        <q-popup-proxy
                          cover
                          transition-show="scale"
                          transition-hide="scale"
                        >
                          <q-date
                            v-model="task.deadline"
                            mask="DD-MM-YYYY HH:mm"
                          >
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
                            v-model="task.deadline"
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
                        v-for="(error, index) in $v.deadline.$errors"
                        :key="index"
                      >
                        <span>{{ error.$message }}</span>
                      </li>
                    </template>
                  </q-input>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-select
                    v-model="task.taskStatusId"
                    :options="taskstatuses"
                    emit-value
                    map-options
                    option-value="id"
                    option-label="status"
                    label="Task Status"
                    :error="$v.taskStatusId.$error"
                    :readonly="authStore.getUser.id != task.creatorId"
                  >
                    <template #error>
                      <li
                        v-for="(error, index) in $v.taskStatusId.$errors"
                        :key="index"
                      >
                        <span>{{ error.$message }}</span>
                      </li>
                    </template>
                  </q-select>
                </q-item-section>
              </q-item>
              <div class="tw-flex tw-items-center tw-justify-center">
                <q-item>
                  <q-item-section>
                    <div class="tw-text-center tw-my-2">Assigned Users</div>
                    <div
                      class="tw-flex tw-space-x-2 tw-items-center tw-justify-center"
                    >
                      <q-badge
                        class="tw-flex tw-items-center tw-justify-center tw-space-x-2"
                        v-for="assigneduser in task.assignedUsers"
                        :key="assigneduser.user.id"
                        :label="assigneduser.user.firstName"
                        @click="
                          removeUserFromAssignedUsers(assigneduser.user.id)
                        "
                      >
                      </q-badge>
                    </div>
                    <span v-if="$v.assignedUsers.$error" class="text-red">
                      <div
                        class="tw-flex tw-items-center tw-justify-center"
                        v-for="(error, index) in $v.assignedUsers.$errors"
                        :key="index"
                      >
                        <span>{{ error.$message }}</span>
                      </div>
                    </span>
                  </q-item-section>
                </q-item>
              </div>
              <q-item v-if="task.creatorId == authStore.getUser.id && !task.assignedUsers.some((x) => x.user.id === authStore.getUser.id)">
                <q-item-section>
                  <div class="tw-flex tw-justify-center tw-items-center">
                    <q-btn @click="assignMe" label="Assign Me"></q-btn>
                  </div>
                </q-item-section>
              </q-item>
              <q-item v-if="task.creatorId == authStore.getUser.id">
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
              <q-item>
                <q-item-section>
                  <div
                    class="tw-pt-5"
                    v-if="task.comments && task.comments.length > 0"
                  >
                    <div
                      class="tw-text-xl tw-flex tw-justify-center lg:tw-text-left tw-text-black tw-my-2"
                    >
                      Comments
                    </div>
                    <div class="tw-flex tw-flex-col tw-rounded-lg">
                      <div
                        class="tw-flex tw-justify-between tw-items-center tw-my-2 tw-border tw-rounded-md tw-px-2 tw-py-3"
                        v-for="comment in task.comments"
                        :key="comment.id"
                      >
                        <div class="tw-flex tw-flex-col tw-gap-2 tw-w-full">
                          <div>
                            {{ comment.user.firstName }}
                            {{ comment.user.lastName }} |
                            {{ formatDate(comment.commentDate) }}
                          </div>
                          <div
                            class="tw-flex tw-justify-between tw-items-center"
                            v-if="!editcommentclicked(comment.id)"
                          >
                            <div>
                              {{ comment.description }}
                            </div>
                            <div class="tw-flex tw-space-x-2">
                              <q-btn
                                size="12px"
                                v-if="controlUser(comment.userId)"
                                @click="
                                  editcomment(comment.id, comment.description)
                                "
                                flat
                                dense
                                round
                                icon="edit"
                              />
                              <q-btn
                                size="12px"
                                v-if="controlUser(comment.userId)"
                                @click="removeComment(comment.id)"
                                flat
                                dense
                                round
                                icon="clear"
                              />
                            </div>
                          </div>
                          <q-item-label
                            v-if="editcommentclicked(comment.id) && editing"
                          >
                            <q-input
                              autofocus
                              dense
                              :value="editcommentdata.description"
                              v-model="editcommentdata.description"
                            >
                              <template v-slot:after>
                                <q-btn
                                  flat
                                  dense
                                  color="negative"
                                  icon="cancel"
                                  @click="cancelEditing"
                                />
                                <q-btn
                                  flat
                                  dense
                                  color="positive"
                                  icon="check_circle"
                                  :disable="
                                    !editcommentdata.description ||
                                    editcommentdata.description ===
                                      comment.description ||
                                    editcommentdata.id < 0 ||
                                    editcommentdata.description.length < 5
                                  "
                                  @click="updateComment()"
                                />
                              </template>
                            </q-input>
                          </q-item-label>
                        </div>
                      </div>
                    </div>
                  </div>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-editor
                    v-model="comment"
                    min-height="4rem"
                    placeholder="Comment"
                  >
                  </q-editor>
                  <span v-if="comment.length < 5" class="text-red"
                    >Length must be minimum 5</span
                  >
                </q-item-section>
              </q-item>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn
            :disable="!comment || comment.length < 5"
            @click="sendComment()"
            label="Send Comment"
            color="primary"
          />
          <q-btn
            v-if="task.creatorId == authStore.getUser.id"
            @click="updateTask()"
            :disable="$v.$invalid"
            label="Update"
            color="primary"
          />
          <q-btn
            v-if="task.creatorId == authStore.getUser.id"
            @click="removeTask(task.id)"
            label="Remove"
            color="secondary"
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-scroll-area>
</template>

<script setup lang="ts">
import { useTaskStore } from 'src/stores/Task';
import Alert from './Alert.vue';
import { computed, ref, onMounted } from 'vue';
import { useAuthStore } from 'src/stores/Auth';
import { useCommentStore } from 'src/stores/Comment';
import { TaskDetailDTO } from 'src/models/Task/TaskDetailDTO';
import { UpdateTask } from 'src/models/Task/UpdateTask';
import { date } from 'quasar';
import { CreateComment } from 'src/models/Comment/CreateComment';
import { TaskStatusDTO } from 'src/models/TaskStatus/TaskStatusDTO';
import { useTaskStatusStore } from 'src/stores/TaskStatus';
import { UpdateComment } from 'src/models/Comment/UpdateComment';
import useNotify from 'src/composables/useNotify';
import useVuelidate from '@vuelidate/core';
import { required, helpers, minLength } from '@vuelidate/validators';

const email = ref<string>('');
const taskStatusStore = useTaskStatusStore();
onMounted(async () => {
  await taskStatusStore.getTaskStatuses();
});

const editing = ref<boolean>(false);
const editcommentdata = ref<UpdateComment>({} as UpdateComment);
const task = computed<TaskDetailDTO>(() => {
  return taskStore.getTaskDetailFromState;
});

const taskstatuses = computed<TaskStatusDTO[]>(() => {
  return taskStatusStore.getTaskStatusesFromState;
});

const { notifySuccess } = useNotify();

const commentStore = useCommentStore();
const comment = ref<string>('');
const taskStore = useTaskStore();

const authStore = useAuthStore();

function assignMe() {
  var user = authStore.getUser;
  if (user.id == task.value.creatorId) {
    if (user && !task.value.assignedUsers.some((x) => x.userId === user.id)) {
      task.value.assignedUsers.push({
        userId: user.id,
        user: user,
        taskId: task.value.id,
      });
    }
  }
}

function controlUser(userid: string) {
  return userid == authStore.getUser.id;
}

function editcomment(id: number, description: string) {
  editing.value = true;
  editcommentdata.value.id = id;
  editcommentdata.value.description = description;
}

function editcommentclicked(commentid: number) {
  return commentid === editcommentdata.value.id;
}

async function updateTask() {
  var updateTask: UpdateTask = {
    id: task.value.id,
    title: task.value.title,
    description: task.value.description,
    stringdeadline: task.value.deadline,
    userIds: task.value.assignedUsers.map((x) => x.user.id),
    taskStatusId: task.value.taskStatusId,
  };
  const responsemessage = await taskStore.updateTask(updateTask);
  if (responsemessage) {
    resetTaskDetail();
    notifySuccess(responsemessage);
  }
}

async function removeTask(taskId: number) {
  const responsemessage = await taskStore.removeTask(taskId);
  if (responsemessage) {
    resetTaskDetail();
    notifySuccess(responsemessage);
  }
}

async function searchUserByEmail(email: string) {
  if (email) {
    await authStore.getUserByEmail(email);
    var searcheduser = authStore.getSearchedUserFromState;
    if (
      searcheduser.id &&
      !task.value.assignedUsers.some((x) => x.userId === searcheduser.id)
    ) {
      task.value.assignedUsers.push({
        userId: searcheduser.id,
        user: searcheduser,
        taskId: task.value.id,
      });
    }
  }
}

function removeUserFromAssignedUsers(id: string) {
  if (authStore.getUser.id == task.value.creatorId) {
    var existingIndex = task.value.assignedUsers.findIndex(
      (x) => x.userId === id
    );
    if (existingIndex > -1) {
      task.value.assignedUsers.splice(existingIndex, 1);
    }
  }
}

function resetTaskDetail() {
  taskStore.clearSelectedTaskId();
  email.value = '';
}

function formatDate(commentdate: Date) {
  return date.formatDate(commentdate, 'DD-MM-YYYY HH:mm');
}

async function sendComment() {
  if (comment.value) {
    var createComment: CreateComment = {
      description: comment.value,
      taskId: task.value.id,
    };
    var responsemessage = await commentStore.addComment(createComment);
    if (responsemessage) {
      notifySuccess(responsemessage);
      comment.value = '';
    }
  }
}

async function updateComment() {
  if (editcommentdata.value.description && editcommentdata.value.id > 0) {
    var updateComment: UpdateComment = {
      id: editcommentdata.value.id,
      description: editcommentdata.value.description,
    };
    var responsemessage = await commentStore.updateComment(updateComment);
    if (responsemessage) {
      notifySuccess(responsemessage);
      editing.value = false;
      editcommentdata.value = {} as UpdateComment;
    }
  }
}

function cancelEditing() {
  editing.value = false;
  editcommentdata.value.id = 0;
  editcommentdata.value.description = '';
}

async function removeComment(id: number) {
  await commentStore.removeComment(id);
}

const rules = computed(() => {
  return {
    title: {
      required: helpers.withMessage('Title is required', required),
    },
    description: {
      required: helpers.withMessage('Description is required', required),
    },
    assignedUsers: {
      required: helpers.withMessage('Atleast one user is required', required),
      minLength: helpers.withMessage(
        'Atleast one user is required',
        minLength(1)
      ),
    },
    taskStatusId: {
      required: helpers.withMessage('Task status is required', required),
    },
    deadline: {
      required: helpers.withMessage('Deadline is required', required),
    },
  };
});

const $v = useVuelidate(rules, task, { $autoDirty: true });
</script>

<style scoped></style>

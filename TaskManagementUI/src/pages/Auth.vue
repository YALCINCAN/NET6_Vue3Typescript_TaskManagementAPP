<template>
  <div
    class="tw-min-h-screen tw-flex tw-flex-col tw-items-center tw-justify-center"
  >
    <div>
      <q-tabs v-model="tab" narrow-indicator class="tw-py-2">
        <q-tab class="text-purple" name="login" label="Login" />
        <q-tab class="text-orange" name="register" label="Register" />
      </q-tabs>
      <div class="tw-w-[400px] md:tw-w-[1000px]">
        <q-tab-panels v-model="tab" class="text-black text-center">
          <q-tab-panel name="login">
            <div
              class="tw-max-w-lg tw-mx-auto tw-flex-1 tw-flex tw-flex-col tw-items-center tw-justify-center tw-px-2"
            >
              <div
                class="tw-bg-white tw-px-6 tw-py-8 tw-rounded lg:tw-shadow-md tw-text-black tw-w-full"
              >
                <Alert></Alert>
                <q-input
                  outlined
                  type="text"
                  v-model="logincredentials.username"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="Username"
                  :error="$vlogin.username.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vlogin.username.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
                <q-input
                  outlined
                  type="password"
                  v-model="logincredentials.password"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="Password"
                  :error="$vlogin.password.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vlogin.password.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>

                <q-btn
                  type="submit"
                  @click="login()"
                  :disable="$vlogin.$invalid"
                  class="tw-w-full tw-text-center tw-py-3 tw-rounded tw-bg-green-500 tw-text-white hover:tw-bg-green-700 focus:tw-outline-none tw-my-1"
                >
                  Login
                </q-btn>
              </div>
            </div>
          </q-tab-panel>

          <q-tab-panel name="register">
            <div
              class="tw-max-w-lg tw-mx-auto tw-flex-1 tw-flex tw-flex-col tw-items-center tw-justify-center tw-px-2"
            >
              <div
                class="tw-bg-white tw-px-6 tw-py-8 tw-rounded tw-shadow-md tw-text-black tw-w-full"
              >
                <Alert></Alert>
                <q-input
                  outlined
                  v-model="registercredentials.username"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="Username"
                  :error="$vregister.username.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vregister.username.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
                <q-input
                  outlined
                  type="text"
                  v-model="registercredentials.firstName"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="First Name"
                  :error="$vregister.firstName.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vregister.firstName.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
                <q-input
                  outlined
                  type="text"
                  v-model="registercredentials.lastName"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="Last Name"
                  :error="$vregister.lastName.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vregister.lastName.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
                <q-input
                  outlined
                  type="password"
                  v-model="registercredentials.password"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="Password"
                  :error="$vregister.password.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vregister.password.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>
                <q-input
                  outlined
                  type="password"
                  v-model="registercredentials.confirmpassword"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="Confirm Password"
                  :error="$vregister.confirmpassword.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vregister.confirmpassword
                        .$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>

                <q-input
                  outlined
                  type="email"
                  v-model="registercredentials.email"
                  class="tw-block tw-w-full tw-p-3 tw-mb-4"
                  label="Email"
                  :error="$vregister.email.$error"
                >
                  <template #error>
                    <li
                      v-for="(error, index) in $vregister.email.$errors"
                      :key="index"
                    >
                      <span>{{ error.$message }}</span>
                    </li>
                  </template>
                </q-input>

                <button
                  type="submit"
                  @click="register"
                  :disable="$vregister.$invalid"
                  class="tw-w-full tw-text-center tw-py-3 tw-rounded tw-bg-green-500 tw-text-white hover:tw-bg-green-700 focus:tw-outline-none tw-my-1"
                >
                  Register
                </button>
              </div>

              <div class="tw-text-grey-dark tw-mt-6">
                Already have an account?
                <a
                  class="tw-no-underline tw-border-b tw-text-blue-700 hover:tw-text-purple-500 hover:tw-cursor-pointer"
                  @click="tab = 'login'"
                >
                  Login </a
                >.
              </div>
            </div>
          </q-tab-panel>
        </q-tab-panels>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { Login } from 'src/models/Auth/Login';
import { Register } from 'src/models/Auth/Register';
import { reactive, computed } from 'vue';
import { oneUpperCase } from 'src/utilities/validators';
import { useRouter } from 'vue-router';
import useVuelidate from '@vuelidate/core';
import { useAlertStore } from 'src/stores/Alert';
import {
  required,
  helpers,
  minLength,
  sameAs,
  email,
} from '@vuelidate/validators';
import { useAuthStore } from 'src/stores/Auth';
import Alert from 'src/components/Alert.vue';

const tab = ref('login');
const logincredentials = reactive<Login>({} as Login);
const registercredentials = reactive<Register>({} as Register);
const authStore = useAuthStore();
const alertStore = useAlertStore();
const router = useRouter();
async function login() {
  var response = await authStore.login(logincredentials);
  if (response) {
    router.push('/');
  }
}
async function register() {
  await authStore.register(registercredentials);
}
const loginrules = computed(() => {
  return {
    username: {
      required: helpers.withMessage('Username is required', required),
    },
    password: {
      required: helpers.withMessage('Password is required', required),
    },
  };
});

const registerrules = computed(() => {
  return {
    username: {
      required: helpers.withMessage('Username is required', required),
    },
    firstName: {
      required: helpers.withMessage('First Name is required', required),
    },
    lastName: {
      required: helpers.withMessage('Last Name is required', required),
    },
    password: {
      required: helpers.withMessage('Password is required', required),
      minLength: minLength(9),
      oneUpperCase: helpers.withMessage(
        'Password need one must upper case',
        oneUpperCase
      ),
    },
    confirmpassword: {
      required: helpers.withMessage('Confirm password is required', required),
      sameAs: helpers.withMessage(
        'Confirm password dont match with password',
        sameAs(registercredentials.password)
      ),
    },
    email: {
      required: helpers.withMessage('Email is required', required),
      email: email,
    },
  };
});
const $vlogin = useVuelidate(loginrules, logincredentials, {
  $autoDirty: true,
});
const $vregister = useVuelidate(registerrules, registercredentials, {
  $autoDirty: true,
});
</script>

<style scoped></style>

import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'Index',
        component: () => import('pages/IndexPage.vue'),
        meta: { requiresLogin: true },
      },
      {
        path: '/auth',
        name: 'Auth',
        component: () => import('pages/Auth.vue'),
      },
      {
        path: '/confirmemail/:userId/:token',
        name: 'ConfirmEmail',
        component: () => import('pages/ConfirmEmail.vue'),
      },
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;

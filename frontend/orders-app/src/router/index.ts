import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'

import OrdersList from '@/components/OrdersList.vue'
import OrderForm from '@/components/OrderForm.vue'
import OrderDetails from '@/components/OrderDetails.vue'


import ClientsList from '@/components/ClientsList.vue'
import ClientForm from '@/components/ClientForm.vue'

const routes: RouteRecordRaw[] = [
  { path: '/', redirect: '/orders' },

  { path: '/orders', component: OrdersList },
  { path: '/orders/new', component: OrderForm },
  { path: '/orders/:id', component: OrderDetails },
  { path: '/orders/:id/edit', component: OrderForm, props: true },

  { path: '/clients', component: ClientsList },
  { path: '/clients/new', component: ClientForm },
  { path: '/clients/:id/edit', component: ClientForm, props: true }
]

export default createRouter({
  history: createWebHistory(),
  routes
})

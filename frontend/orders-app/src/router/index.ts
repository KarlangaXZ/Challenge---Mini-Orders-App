import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import OrdersList from '@/components/OrdersList.vue'
import OrderForm from '@/components/OrderForm.vue'
import OrderDetails from '@/components/OrderDetails.vue'

const routes: RouteRecordRaw[] = [
  { path: '/', redirect: '/orders' },
  { path: '/orders', component: OrdersList },
  { path: '/orders/new', component: OrderForm },
  { path: '/orders/:id', component: OrderDetails }
]

export default createRouter({
  history: createWebHistory(),
  routes
})




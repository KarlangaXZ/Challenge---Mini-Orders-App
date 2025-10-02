<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useRouter } from 'vue-router'
import api from '../services/api'

interface Order {
  id: string
  cliente: string
  fecha: string
  total: number
}

const orders = ref<Order[]>([])
const total = ref(0)
const page = ref(1)
const pageSize = 10
const search = ref('')
const router = useRouter()

async function fetchOrders() {
  const { data } = await api.get('/orders', {
    params: { search: search.value, page: page.value, pageSize }
  })
  orders.value = data.items
  total.value = data.total
}

onMounted(fetchOrders)
watch([search, page], fetchOrders)
</script>

<template>
  <div class="container py-4 ">
    <h1 class="h3 mb-4">Órdenes</h1>

    <div class="row mb-3">
      <div class="col">
        <input v-model="search" class="form-control" placeholder="Buscar cliente…">
      </div>
      <div class="col-auto">
        <router-link to="/orders/new" class="btn btn-primary">Nueva Orden</router-link>
      </div>
    </div>

    <table class="table table-striped">
      <thead>
        <tr>
          <th>Cliente</th>
          <th>Fecha</th>
          <th>Total</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="o in orders" :key="o.id">
          <td>{{ o.cliente }}</td>
          <td>{{ new Date(o.fecha).toLocaleDateString() }}</td>
          <td>${{ o.total.toFixed(2) }}</td>
          <td>
            <button class="btn btn-info btn-sm"
                    @click="router.push(`/orders/${o.id}`)">
              Ver
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

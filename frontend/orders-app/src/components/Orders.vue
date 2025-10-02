<script setup lang="ts">
import { ref, onMounted } from 'vue'
import api from '../services/api'

interface Order {
  id: string
  cliente: string
  fecha: string
  total: number
}

const orders = ref<Order[]>([])

onMounted(async () => {
  const res = await api.get('/orders')
  orders.value = res.data.items 
})
</script>

<template>
  <div class="p-6">
    <h1 class="text-2xl font-bold mb-4">📋 Lista de Órdenes</h1>
    <table class="min-w-full border border-gray-300 rounded-lg">
      <thead class="bg-gray-200">
        <tr>
          <th class="px-4 py-2">Cliente</th>
          <th class="px-4 py-2">Fecha</th>
          <th class="px-4 py-2">Total</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="o in orders" :key="o.id" class="border-t">
          <td class="px-4 py-2">{{ o.cliente }}</td>
          <td class="px-4 py-2">{{ new Date(o.fecha).toLocaleDateString() }}</td>
          <td class="px-4 py-2">${{ o.total.toFixed(2) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

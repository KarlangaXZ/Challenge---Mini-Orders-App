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
const error = ref('')

onMounted(async () => {
  try {
    const res = await api.get('/orders')
    orders.value = res.data // Ajusta si tu backend devuelve { items: [...] }
  } catch (err: any) {
    error.value = err.message || 'Error al cargar las órdenes'
  }
})
</script>

<template>
  <div class="container mt-4">
    <h1 class="mb-4">📋 Lista de Órdenes</h1>
    <div v-if="error" class="alert alert-danger">{{ error }}</div>
    <table class="table table-bordered table-striped" v-else>
      <thead class="table-dark">
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
            <button class="btn btn-primary btn-sm">Ver</button>
            <button class="btn btn-warning btn-sm">Editar</button>
            <button class="btn btn-danger btn-sm">Eliminar</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

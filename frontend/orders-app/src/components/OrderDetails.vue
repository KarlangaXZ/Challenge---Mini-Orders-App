<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import api from '../services/api'

interface Order {
  id: string
  cliente: string
  fecha: string
  total: number
}

const route = useRoute()
const router = useRouter()
const order = ref<Order | null>(null)
const errorMsg = ref('')

async function fetchOrder() {
  const id = route.params.id as string
  try {
    const res = await api.get(`/orders/${id}`)
    order.value = res.data
  } catch {
    errorMsg.value = 'No se pudo cargar la orden.'
  }
}

onMounted(fetchOrder)
</script>

<template>
  <div class="container py-4">
    <h1 class="h4 mb-4">Detalle de Orden</h1>
    <div v-if="errorMsg" class="alert alert-danger">{{ errorMsg }}</div>
    <div v-if="order">
      <p><strong>Cliente:</strong> {{ order.cliente }}</p>
      <p><strong>Fecha:</strong> {{ new Date(order.fecha).toLocaleDateString() }}</p>
      <p><strong>Total:</strong> ${{ order.total.toFixed(2) }}</p>
      <button class="btn btn-secondary" @click="router.back()">Volver</button>
    </div>
  </div>
</template>

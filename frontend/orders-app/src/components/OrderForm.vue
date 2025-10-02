<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../services/api'

interface OrderCreateDto {
  cliente: string
  fecha?: string
  total: number
}

const router = useRouter()
const form = ref<OrderCreateDto>({
  cliente: '',
  fecha: '',
  total: 0
})
const errorMsg = ref('')

async function submit() {
  errorMsg.value = ''
 
  if (!form.value.cliente.trim()) {
    errorMsg.value = 'El cliente es obligatorio.'
    return
  }
  if (form.value.total <= 0) {
    errorMsg.value = 'El total debe ser mayor que 0.'
    return
  }
  try {
    await api.post('/orders', form.value)
    router.push('/orders') 
  } catch (err: any) {

    errorMsg.value = err.response?.data?.message ?? 'Ocurrió un error al guardar.'
  }
}
</script>

<template>
  <div class="container py-4">
    <h1 class="h4 mb-4">Nueva Orden</h1>

    <!-- Mensaje de error -->
    <div v-if="errorMsg" class="alert alert-danger" role="alert">
      {{ errorMsg }}
    </div>

    <form @submit.prevent="submit">
      <div class="mb-3">
        <label class="form-label">Cliente</label>
        <input v-model="form.cliente" class="form-control" required>
      </div>
      <div class="mb-3">
        <label class="form-label">Fecha</label>
        <input v-model="form.fecha" type="date" class="form-control">
      </div>
      <div class="mb-3">
        <label class="form-label">Total</label>
        <input v-model.number="form.total" type="number" min="0" step="0.01" class="form-control" required>
      </div>
      <button type="submit" class="btn btn-success">Guardar</button>
    </form>
  </div>
</template>

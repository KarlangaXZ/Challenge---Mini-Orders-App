<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import api from '@/services/api'

interface Order {
  id: string
  cliente: string
  fecha: string
  total: number
}

const route = useRoute()
const router = useRouter()

const fromName = ref<string>('') 
const toName = ref<string>('')   
const loading = ref(false)
const info = ref('')
const error = ref('')
const affectedCount = ref<number | null>(null)


onMounted(() => {
  const idParam = route.params.id as string | undefined
  if (idParam) {
    const decoded = decodeURIComponent(idParam)
    fromName.value = decoded
  }
})

const renameClient = async () => {
  info.value = ''
  error.value = ''
  affectedCount.value = null

  const oldName = fromName.value.trim()
  const newName = toName.value.trim()

  if (!oldName || !newName) {
    error.value = 'Completa ambos campos: De y A'
    return
  }
  if (oldName === newName) {
    error.value = 'El nuevo nombre debe ser diferente'
    return
  }

  loading.value = true
  try {
 
    const res = await api.get('/orders')
    const orders: Order[] = Array.isArray(res.data) ? res.data : res.data.items


    const target = orders.filter(o => (o.cliente ?? '').trim() === oldName)
    if (target.length === 0) {
      info.value = `No se encontraron órdenes para el cliente "${oldName}".`
      affectedCount.value = 0
      return
    }

    await Promise.all(
      target.map(o =>
        api.put(`/orders/${o.id}`, {
          cliente: newName,
          fecha: o.fecha,
          total: o.total,  
        })
      )
    )

    affectedCount.value = target.length
    info.value = `Cliente renombrado en ${target.length} orden(es).`
  } catch (e: any) {
    error.value = e?.response?.data?.message ?? e?.message ?? 'Error al renombrar cliente'
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="container mt-4" style="max-width: 640px;">
    <div class="d-flex align-items-center justify-content-between mb-3">
      <h1 class="h4 mb-0">🔁 Renombrar cliente</h1>
      <router-link class="btn btn-outline-secondary btn-sm" to="/clients">Volver</router-link>
    </div>

    <div v-if="error" class="alert alert-danger">{{ error }}</div>
    <div v-if="info" class="alert alert-info">{{ info }}</div>

    <form @submit.prevent="renameClient" class="card p-3 border-0 shadow-sm">
      <div class="mb-3">
        <label class="form-label">De (nombre actual)</label>
        <input class="form-control" v-model="fromName" placeholder="Ej: Juan Pérez" />
      </div>

      <div class="mb-3">
        <label class="form-label">A (nuevo nombre)</label>
        <input class="form-control" v-model="toName" placeholder="Ej: Juan P. García" />
      </div>

      <button class="btn btn-primary" type="submit" :disabled="loading">
        {{ loading ? 'Aplicando cambios…' : 'Renombrar en todas las órdenes' }}
      </button>

      <p v-if="affectedCount !== null" class="mt-3 text-muted">
        Órdenes afectadas: <strong>{{ affectedCount }}</strong>
      </p>
    </form>
  </div>
</template>

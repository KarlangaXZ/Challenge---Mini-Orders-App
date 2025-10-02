<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import api from '@/services/api'

interface Order {
  id: string
  cliente: string
  fecha: string
  total: number
}

const router = useRouter()

const orders = ref<Order[]>([])
const loading = ref(true)
const error = ref('')
const search = ref('')

const fetchOrders = async () => {
  loading.value = true
  error.value = ''
  try {
    const res = await api.get('/orders')
    orders.value = Array.isArray(res.data) ? res.data : res.data.items
  } catch (e: any) {
    error.value = e?.message ?? 'Error al cargar órdenes'
  } finally {
    loading.value = false
  }
}
onMounted(fetchOrders)

type ClientRow = { name: string; count: number }

const clients = computed<ClientRow[]>(() => {
  const map = new Map<string, number>()
  for (const o of orders.value) {
    const name = (o.cliente ?? '').trim()
    if (!name) continue
    map.set(name, (map.get(name) ?? 0) + 1)
  }
  let list = Array.from(map, ([name, count]) => ({ name, count }))
  if (search.value) {
    const q = search.value.toLowerCase()
    list = list.filter(c => c.name.toLowerCase().includes(q))
  }
  return list.sort((a, b) => a.name.localeCompare(b.name))
})

const goEdit = (clientName: string) => {
  router.push(`/clients/${encodeURIComponent(clientName)}/edit`)
}
</script>

<template>
  <div class="container mt-4">
    <div class="d-flex align-items-center justify-content-between mb-3">
      <h1 class="h4 mb-0">👤 Clientes</h1>
      <router-link class="btn btn-outline-primary btn-sm" to="/orders">Volver a Órdenes</router-link>
    </div>

    <div class="mb-3">
      <input class="form-control" placeholder="Buscar cliente…" v-model="search" />
    </div>

    <div v-if="error" class="alert alert-danger">{{ error }}</div>
    <div v-else-if="loading" class="alert alert-secondary">Cargando…</div>

    <div v-else class="table-responsive">
      <table class="table table-striped table-bordered align-middle">
        <thead class="table-dark">
          <tr>
            <th>Nombre</th>
            <th class="text-center"># Órdenes</th>
            <th style="width: 180px">Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="c in clients" :key="c.name">
            <td><strong>{{ c.name }}</strong></td>
            <td class="text-center">{{ c.count }}</td>
            <td>
              <button class="btn btn-warning btn-sm me-2" @click="goEdit(c.name)">
                Renombrar
              </button>
            </td>
          </tr>
          <tr v-if="clients.length === 0">
            <td colspan="3" class="text-center text-muted">No hay clientes (¿aún no hay órdenes?).</td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="mt-3">
      <router-link class="btn btn-primary btn-sm" to="/clients/new">
        Renombrar en lote (manual)
      </router-link>
    </div>
  </div>
</template>

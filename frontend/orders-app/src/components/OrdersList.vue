
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import api from '../services/api'

interface Order {
  id: string
  cliente: string
  fecha: string   
  total: number
}

const orders = ref<Order[]>([])
const loading = ref(true)
const error = ref('')


const editing = ref<Order | null>(null)
const saving = ref(false)


const editingDate = computed({
  get() {
    if (!editing.value) return ''
    
    return new Date(editing.value.fecha).toISOString().slice(0, 10)
  },
  set(v: string) {
    if (editing.value) editing.value.fecha = v 
  }
})

const fetchOrders = async () => {
  loading.value = true
  error.value = ''
  try {
    const res = await api.get('/orders')
    orders.value = Array.isArray(res.data) ? res.data : res.data.items
  } catch (e: any) {
    error.value = e?.message ?? 'Error al cargar las órdenes'
  } finally {
    loading.value = false
  }
}

onMounted(fetchOrders)

const openEdit = (o: Order) => {
  editing.value = { ...o }
  const el = document.getElementById('editModal')!
  const modal = new (window as any).bootstrap.Modal(el)
  modal.show()
}

const saveEdit = async () => {
  if (!editing.value) return
  if (!editing.value.cliente || !editing.value.fecha || editing.value.total <= 0) {
    alert('Completa todos los campos. El total debe ser mayor que 0.')
    return
  }
  saving.value = true
  try {
    await api.put(`/orders/${editing.value.id}`, {
      cliente: editing.value.cliente,
      fecha: editing.value.fecha, 
      total: editing.value.total,
    })
    
    const i = orders.value.findIndex(o => o.id === editing.value!.id)
    if (i !== -1) orders.value[i] = { ...editing.value }
    ;(window as any).bootstrap.Modal
      .getInstance(document.getElementById('editModal')!)
      ?.hide()
  } catch (e: any) {
    alert(e?.response?.data?.message ?? e?.message ?? 'Error al guardar')
  } finally {
    saving.value = false
  }
}

const removeOrder = async (id: string) => {
  if (!confirm('¿Eliminar esta orden?')) return
  try {
    await api.delete(`/orders/${id}`)
    orders.value = orders.value.filter(o => o.id !== id)
  } catch (e: any) {
    alert(e?.response?.data?.message ?? e?.message ?? 'Error al eliminar')
  }
}
</script>

<template>
  <div class="container mt-4">
    <div class="d-flex align-items-center justify-content-between mb-3">
      <h1 class="h3 mb-0">📋 Lista de Órdenes</h1>
      <router-link to="/orders/new" class="btn btn-success">Nueva orden</router-link>
    </div>

    <div v-if="error" class="alert alert-danger">{{ error }}</div>
    <div v-else-if="loading" class="alert alert-secondary">Cargando órdenes…</div>
    <table v-else class="table table-bordered table-striped">
      <thead class="table-dark">
        <tr>
          <th>Cliente</th>
          <th>Fecha</th>
          <th class="text-end">Total</th>
          <th style="width: 220px">Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="o in orders" :key="o.id">
          <td>{{ o.cliente }}</td>
          <td>{{ new Date(o.fecha).toLocaleDateString() }}</td>
          <td class="text-end">${{ o.total.toFixed(2) }}</td>
          <td>
            <button class="btn btn-warning btn-sm me-2" @click="openEdit(o)">Modificar</button>
            <button class="btn btn-danger btn-sm" @click="removeOrder(o.id)">Eliminar</button>
          </td>
        </tr>
        <tr v-if="orders.length === 0">
          <td colspan="4" class="text-center text-muted">No hay órdenes</td>
        </tr>
      </tbody>
    </table>
  </div>

 
  <div class="modal fade" id="editModal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog">
      <form class="modal-content" @submit.prevent="saveEdit">
        <div class="modal-header">
          <h5 class="modal-title">Modificar orden / cliente</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
        </div>
        <div class="modal-body" v-if="editing">
          <div class="mb-3">
            <label class="form-label">Cliente</label>
            <input type="text" class="form-control" v-model="editing.cliente" required />
          </div>
          <div class="mb-3">
            <label class="form-label">Fecha</label>
            <input type="date" class="form-control" v-model="editingDate" required />
          </div>
          <div class="mb-3">
            <label class="form-label">Total</label>
            <input type="number" step="0.01" class="form-control" v-model.number="editing.total" min="0.01" required />
          </div>
        </div>
        <div class="modal-footer">
          <button class="btn btn-secondary" type="button" data-bs-dismiss="modal">Cancelar</button>
          <button class="btn btn-primary" type="submit" :disabled="saving">
            {{ saving ? 'Guardando…' : 'Guardar cambios' }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

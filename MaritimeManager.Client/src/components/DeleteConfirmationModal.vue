<script setup lang="ts">
import { ref } from 'vue'
import API_BASE_URL from '@/api'

const emit = defineEmits<{
  (e: 'poi-deleted'): void
}>()

const dialogElement = ref<HTMLDialogElement>()
const poiIdentifier = ref<string>('')
const poiName = ref<string>('')

const openModal = (identifier: string, name: string) => {
  poiIdentifier.value = identifier
  poiName.value = name
  dialogElement.value?.showModal()
}

const closeModal = () => {
  dialogElement.value?.close()
}

const handleDelete = async () => {
  try {
    const response = await fetch(`${API_BASE_URL}/${poiIdentifier.value}`, {
      method: 'DELETE',
    })

    if (!response.ok) throw new Error('Failed to delete POI')

    closeModal()
    emit('poi-deleted')
  } catch (error) {
    console.error('Failed to delete POI:', error)
    // TODO: Add user-facing error message
  }
}

defineExpose({
  openModal,
})
</script>

<template>
  <dialog ref="dialogElement" class="modal-dialog">
    <header class="flex justify-between items-center mb-4">
      <h2 class="text-xl font-bold text-red-600">Delete Point of Interest</h2>
      <button @click="closeModal" class="text-gray-500 hover:text-gray-700 text-2xl leading-none">
        &times;
      </button>
    </header>

    <div class="mb-6">
      <p class="text-gray-700">
        Are you sure you want to delete <strong>{{ poiName }}</strong>?
      </p>
      <p class="text-gray-600 text-sm mt-2">This action cannot be undone.</p>
    </div>

    <div class="flex justify-end space-x-3">
      <button
        type="button"
        @click="closeModal"
        class="px-4 py-2 bg-gray-300 text-gray-700 rounded-md hover:bg-gray-400"
      >
        Cancel
      </button>
      <button
        type="button"
        @click="handleDelete"
        class="px-4 py-2 bg-red-600 text-white rounded-md hover:bg-red-700"
      >
        Delete
      </button>
    </div>
  </dialog>
</template>

<style scoped>
.modal-dialog {
  &::backdrop {
    background-color: rgba(0, 0, 0, 0.5);
  }
  border: none;
  padding: 14pt;
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 999;
  background-color: white;
  border-radius: 0.5rem;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
  max-width: 500px;
  width: 90vw;
}
</style>


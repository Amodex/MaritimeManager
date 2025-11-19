<script setup lang="ts">
import { ref, reactive, computed } from 'vue'
import API_BASE_URL from '@/api'
import { PointOfInterestType } from '@/enums/PointOfInterestType.ts'
import type { PointOfInterestInterface } from '@/interfaces/PointOfInterestInterface.ts'

const emit = defineEmits<{
  (e: 'poi-updated'): void
}>()

const dialogElement = ref<HTMLDialogElement>()

const poiTypes = computed(() => {
  return Object.entries(PointOfInterestType)
    .filter(([key]) => isNaN(Number(key)))
    .map(([name, value]) => ({
      name: name,
      value: value
    }));
});

const form = reactive({
  identifier: '',
  name: '',
  description: '',
  latitude: 0,
  longitude: 0,
  type: PointOfInterestType.Lighthouse,
})

const openModal = (poi: PointOfInterestInterface) => {
  form.identifier = poi.identifier
  form.name = poi.name
  form.description = poi.description
  form.latitude = poi.latitude
  form.longitude = poi.longitude
  form.type = poi.type as PointOfInterestType
  dialogElement.value?.showModal()
}

const closeModal = () => {
  dialogElement.value?.close()
}

const handleSubmit = async () => {
  try {
    const response = await fetch(`${API_BASE_URL}/${form.identifier}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(form),
    })

    if (!response.ok) throw new Error('Failed to update POI')

    closeModal()
    emit('poi-updated')
  } catch (error) {
    console.error('Failed to update POI:', error)
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
      <h2 class="text-xl font-bold">Edit Point of Interest</h2>
      <button @click="closeModal" class="text-gray-500 hover:text-gray-700 text-2xl leading-none">
        &times;
      </button>
    </header>

    <form @submit.prevent="handleSubmit">
      <div class="mb-4">
        <label for="edit-name" class="block text-sm font-medium text-gray-700">Name:</label>
        <input
          type="text"
          id="edit-name"
          v-model="form.name"
          required
          class="mt-1 p-2 block w-full h-8 rounded-md border-2 border-gray-400 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm"
        />
      </div>

      <div class="mb-4">
        <label for="edit-description" class="block text-sm font-medium text-gray-700"
          >Description:</label
        >
        <input
          type="text"
          id="edit-description"
          v-model="form.description"
          class="mt-1 p-2 block w-full h-8 rounded-md border-2 border-gray-400 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm"
        />
      </div>

      <div class="grid grid-cols-2 gap-4 mb-4">
        <div>
          <label for="edit-latitude" class="block text-sm font-medium text-gray-700">Latitude:</label>
          <input
            type="number"
            id="edit-latitude"
            v-model.number="form.latitude"
            step="any"
            required
            class="mt-1 p-2 block w-full h-8 rounded-md border-2 border-gray-400 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm"
          />
        </div>
        <div>
          <label for="edit-longitude" class="block text-sm font-medium text-gray-700">Longitude:</label>
          <input
            type="number"
            id="edit-longitude"
            v-model.number="form.longitude"
            step="any"
            required
            class="mt-1 p-2 block w-full h-8 rounded-md border-2 border-gray-400 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm"
          />
        </div>
      </div>

      <div class="mb-4">
        <label for="edit-type" class="block text-sm font-medium text-gray-700">Type:</label>
        <select
          id="edit-type"
          v-model="form.type"
          class="mt-1 p-2 block w-full h-10 rounded-md border-2 border-gray-400 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm"
        >
          <option v-for="type in poiTypes" :key="type.value" :value="type.value">
            {{ type.name }}
          </option>
        </select>
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
          type="submit"
          class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
        >
          Update Point
        </button>
      </div>
    </form>
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
  max-width: 90vw;
  max-height: 90vh;
  overflow-y: auto;
}
</style>


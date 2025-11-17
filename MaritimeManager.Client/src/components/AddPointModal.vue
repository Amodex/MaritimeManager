<script setup lang="ts">
import { ref, reactive, computed } from 'vue'
import API_BASE_URL from '@/api'
import { PointOfInterestType } from '@/enums/PointOfInterestType.ts'

const emit = defineEmits<{
  (e: 'poi-added'): void
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

console.log(poiTypes);

const form = reactive({
  name: '',
  description: '',
  latitude: 0,
  longitude: 0,
  type: PointOfInterestType.Lighthouse,
})

const openModal = (lat: number, lng: number) => {
  form.latitude = lat
  form.longitude = lng
  form.name = ''
  form.description = ''
  form.type = PointOfInterestType.Lighthouse
  dialogElement.value?.showModal()
}

const closeModal = () => {
  dialogElement.value?.close()
}

const handleSubmit = async () => {
  try {
    const response = await fetch(API_BASE_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(form),
    })

    if (!response.ok) throw new Error('Failed to create POI')

    closeModal()
    emit('poi-added')
  } catch (error) {
    console.error('Failed to add POI:', error)
    // TODO: Add user-facing error message
  }
}

defineExpose({
  openModal,
})
</script>

<!-- src/components/AddPointModal.vue -->
<template>
  <!-- The native HTML dialog element -->
  <dialog ref="dialogElement" class="modal-dialog">
    <header class="flex justify-between items-center mb-4">
      <h2 class="text-xl font-bold">Add New Point of Interest</h2>
      <button @click="closeModal" class="text-gray-500 hover:text-gray-700 text-2xl leading-none">
        &times;
      </button>
    </header>

    <form @submit.prevent="handleSubmit">
      <div class="mb-4">
        <label for="name" class="block text-sm font-medium text-gray-700">Name:</label>
        <input
          type="text"
          id="name"
          v-model="form.name"
          required
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
        />
      </div>

      <div class="mb-4">
        <label for="description" class="block text-sm font-medium text-gray-700"
          >Description:</label
        >
        <input
          type="text"
          id="description"
          v-model="form.description"
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
        />
      </div>

      <!-- The pre-filled lat/lng fields -->
      <div class="grid grid-cols-2 gap-4 mb-4">
        <div>
          <label for="latitude" class="block text-sm font-medium text-gray-700">Latitude:</label>
          <input
            type="number"
            id="latitude"
            v-model.number="form.latitude"
            step="any"
            required
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
            readonly
          />
        </div>
        <div>
          <label for="longitude" class="block text-sm font-medium text-gray-700">Longitude:</label>
          <input
            type="number"
            id="longitude"
            v-model.number="form.longitude"
            step="any"
            required
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
            readonly
          />
        </div>
      </div>

      <div class="mb-4">
        <label for="type" class="block text-sm font-medium text-gray-700">Type:</label>
        <select
          id="type"
          v-model="form.type"
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
        >
          <!-- Loop through the enum entries -->
          <option v-for="type in poiTypes" :key="type.value" :value="type.value">
            {{ type.name }}
            <!-- This will display "Lighthouse", "Wreck", etc. -->
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
          class="px-4 py-2 bg-indigo-600 text-white rounded-md hover:bg-indigo-700"
        >
          Add Point
        </button>
      </div>
    </form>
  </dialog>
</template>

<style>
.modal-dialog {
  &::backdrop {
    background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent black backdrop */
  }
  border: none;
  padding: 14pt;
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 999; /* Ensure it's on top */
  background-color: white;
  border-radius: 0.5rem;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
  max-width: 90vw;
  max-height: 90vh;
  overflow-y: auto;
}

/* Style the inner content container */
.modal-content {
  padding: 1.5rem;
}
</style>

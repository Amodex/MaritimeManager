
<script setup lang="ts">
import { ref } from 'vue';
import type { PointOfInterest } from '@/types';
import API_BASE_URL from '@/api';

// Type the emit function
const emit = defineEmits<{
  (e: 'poi-added'): void
}>();

// Type the ref with our interface
const poi = ref<PointOfInterest>({
  name: '',
  description: '',
  latitude: 0,
  longitude: 0,
  type: 'Lighthouse'
});

const submitForm = async (): Promise<void> => {
  try {
    const response = await fetch(API_BASE_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(poi.value)
    });

    if (!response.ok) throw new Error('Failed to create POI');

    // Reset form to initial typed state
    poi.value = { name: '', description: '', latitude: 0, longitude: 0, type: 'Lighthouse' };
    emit('poi-added');

  } catch (error) {
    console.error('Failed to add POI:', error);
  }
};
</script>

<template>
  <div class="p-6 bg-white rounded-lg shadow-md">
    <h2 class="text-xl font-bold mb-4">Add New Point</h2>
    <form @submit.prevent="submitForm">
      <!-- ... (the form inputs remain the same, just using v-model) ... -->
      <div class="mb-4">
        <label for="name" class="block text-sm font-medium text-gray-700">Name:</label>
        <input type="text" id="name" v-model="poi.name" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
      </div>
      <div class="mb-4">
        <label for="description" class="block text-sm font-medium text-gray-700">Description:</label>
        <input type="text" id="description" v-model="poi.description" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
      </div>
      <div class="grid grid-cols-2 gap-4 mb-4">
        <div>
          <label for="latitude" class="block text-sm font-medium text-gray-700">Latitude:</label>
          <input type="number" id="latitude" v-model.number="poi.latitude" step="any" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
        </div>
        <div>
          <label for="longitude" class="block text-sm font-medium text-gray-700">Longitude:</label>
          <input type="number" id="longitude" v-model.number="poi.longitude" step="any" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
        </div>
      </div>
      <div class="mb-4">
        <label for="type" class="block text-sm font-medium text-gray-700">Type:</label>
        <select id="type" v-model="poi.type" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
          <option value="Lighthouse">Lighthouse</option>
          <option value="Wreck">Wreck</option>
          <option value="Buoy">Buoy</option>
        </select>
      </div>
      <button type="submit" class="w-full bg-indigo-600 text-white font-bold py-2 px-4 rounded hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
        Add Point
      </button>
    </form>
  </div>
</template>

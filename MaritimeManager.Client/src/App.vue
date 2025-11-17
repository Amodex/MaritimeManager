<script setup lang="ts">
import { ref } from 'vue';
import MaritimeMap from './components/MaritimeMap.vue';
import AddPointModal from '@/components/AddPointModal.vue'

const mapComponent = ref<InstanceType<typeof MaritimeMap> | null>(null);
const addPointModal = ref<InstanceType<typeof AddPointModal> | null>(null);

const refreshMap = (): void => {
  if (mapComponent.value) {
    mapComponent.value.refreshMap();
  }
};

const openAddModal = (lat: number, lng: number): void => {
  if (addPointModal.value) {
    addPointModal.value.openModal(lat, lng);
  }
};
</script>

<template>
  <!-- ... (the template HTML remains exactly the same) ... -->
  <header class="bg-blue-600 text-white p-4 text-center shadow-lg">
    <h1 class="text-3xl font-bold">Maritime Points of Interest</h1>
  </header>

  <main class="flex flex-col lg:flex-row h-screen">
    <section class="w-full">
      <MaritimeMap ref="mapComponent" @map-clicked="openAddModal"/>
    </section>
  </main>
  <AddPointModal ref="addPointModal" @poi-added="refreshMap"/>
</template>

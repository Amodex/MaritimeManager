<script setup lang="ts">
import { ref } from 'vue';
import MaritimeMap from './components/MaritimeMap.vue';
import PoiForm from './components/PoiForm.vue';

// Type the component ref. This is a powerful TypeScript feature in Vue.
const mapComponent = ref<InstanceType<typeof MaritimeMap> | null>(null);

const handlePoiAdded = (): void => {
  if (mapComponent.value) {
    mapComponent.value.refreshMap();
  }
};
</script>

<template>
  <!-- ... (the template HTML remains exactly the same) ... -->
  <header class="bg-blue-600 text-white p-4 text-center shadow-lg">
    <h1 class="text-3xl font-bold">Maritime Points of Interest</h1>
  </header>

  <main class="flex flex-col lg:flex-row h-screen">
    <section class="lg:w-3/4">
      <MaritimeMap ref="mapComponent" />
    </section>
    <aside class="lg:w-1/4 p-4 bg-gray-100 overflow-y-auto">
      <PoiForm @poi-added="handlePoiAdded" />
    </aside>
  </main>
</template>

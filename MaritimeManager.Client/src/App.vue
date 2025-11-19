<script setup lang="ts">
import { ref } from 'vue';
import MaritimeMap from './components/MaritimeMap.vue';
import AddPointModal from '@/components/AddPointModal.vue'
import EditPoiModal from '@/components/EditPoiModal.vue'
import DeleteConfirmationModal from '@/components/DeleteConfirmationModal.vue'
import type { PointOfInterestInterface } from '@/interfaces/PointOfInterestInterface.ts'

const mapComponent = ref<InstanceType<typeof MaritimeMap> | null>(null);
const addPointModal = ref<InstanceType<typeof AddPointModal> | null>(null);
const editPoiModal = ref<InstanceType<typeof EditPoiModal> | null>(null);
const deleteConfirmationModal = ref<InstanceType<typeof DeleteConfirmationModal> | null>(null);

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

const openEditModal = (poi: PointOfInterestInterface): void => {
  if (editPoiModal.value) {
    editPoiModal.value.openModal(poi);
  }
};

const openDeleteModal = (identifier: string, name: string): void => {
  if (deleteConfirmationModal.value) {
    deleteConfirmationModal.value.openModal(identifier, name);
  }
};
</script>

<template>
  <header class="bg-blue-600 text-white p-4 text-center shadow-lg">
    <h1 class="text-3xl font-bold">Maritime Points of Interest</h1>
  </header>

  <main class="flex flex-col lg:flex-row h-screen">
    <section class="w-full">
      <MaritimeMap
        ref="mapComponent"
        @map-clicked="openAddModal"
        @edit-poi="openEditModal"
        @delete-poi="openDeleteModal"
      />
    </section>
  </main>

  <AddPointModal ref="addPointModal" @poi-added="refreshMap"/>
  <EditPoiModal ref="editPoiModal" @poi-updated="refreshMap"/>
  <DeleteConfirmationModal ref="deleteConfirmationModal" @poi-deleted="refreshMap"/>
</template>

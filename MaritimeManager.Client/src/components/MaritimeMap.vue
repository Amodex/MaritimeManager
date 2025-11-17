<script setup lang="ts">
import { onMounted, onUnmounted, Ref } from 'vue';
import L from 'leaflet';
import type { Map } from 'leaflet'; // Import Leaflet types
import type { PointOfInterestInterface } from '../interfaces/PointOfInterestInterface.ts'; // Use @ alias for src
import API_BASE_URL from '@/api';

const emit = defineEmits<{
  (e: 'map-clicked', lat: number, lng: number): void
}>();

// Fix for default marker icon issue
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon-2x.png',
  iconUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon.png',
  shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-shadow.png',
});

let map: Map | undefined;
let poiLayer: L.LayerGroup | undefined;

const loadAndDisplayPois = async (): Promise<void> => {
  if (!poiLayer) return;
  try {
    const response = await fetch(API_BASE_URL);
    if (!response.ok) throw new Error('Network response was not ok');
    const pois: PointOfInterestInterface[] = await response.json(); // Type the response

    poiLayer.clearLayers();
    pois.forEach(poi => {
      const marker = L.marker([poi.latitude, poi.longitude]).addTo(poiLayer);
      marker.bindPopup(`<b>${poi.name}</b><br>${poi.description || ''}<br>Type: ${poi.type}`);
    });
  } catch (error) {
    console.error('Failed to load POIs:', error);
  }
};

const onMapClick = (e: L.LeafletMouseEvent) => {
  const { lat, lng } = e.latlng;

  // Emit the coordinates up to the parent component
  emit('map-clicked', lat, lng);
};

// Expose the function to the parent component
defineExpose({
  refreshMap: loadAndDisplayPois
});

onMounted(() => {
  // Initialize map and layer
  map = L.map('map').setView([50.0, -4.0], 6);
  poiLayer = L.layerGroup().addTo(map);

  L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
  }).addTo(map);

  L.tileLayer('https://tiles.openseamap.org/seamark/{z}/{x}/{y}.png', {
    attribution: 'Map data: &copy; <a href="http://www.openseamap.org">OpenSeaMap</a> contributors'
  }).addTo(map);

  map.on('click', onMapClick)

  loadAndDisplayPois();
});

onUnmounted(() => {
  if (map) {
    map.remove();
  }
});
</script>

<template>
  <div id="map" class="w-full h-full"></div>
</template>


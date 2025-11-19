<script setup lang="ts">
import { onMounted, onUnmounted, Ref } from 'vue';
import L from 'leaflet';
import type { Map } from 'leaflet'; // Import Leaflet types
import type { PointOfInterestInterface } from '../interfaces/PointOfInterestInterface.ts'; // Use @ alias for src
import API_BASE_URL from '@/api';

const emit = defineEmits<{
  (e: 'map-clicked', lat: number, lng: number): void
  (e: 'edit-poi', poi: PointOfInterestInterface): void
  (e: 'delete-poi', identifier: string, name: string): void
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
let poisData: PointOfInterestInterface[] = [];

const loadAndDisplayPois = async (): Promise<void> => {
  if (!poiLayer) return;
  try {
    const response = await fetch(API_BASE_URL);
    if (!response.ok) throw new Error('Network response was not ok');
    const pois: PointOfInterestInterface[] = await response.json(); // Type the response

    // Store POIs data for later access
    poisData = pois;

    poiLayer.clearLayers();
    pois.forEach(poi => {
      const marker = L.marker([poi.latitude, poi.longitude]).addTo(poiLayer);

      // Create popup content with edit and delete buttons
      const popupContent = `
        <div style="min-width: 200px;">
          <div style="margin-bottom: 8px;">
            <b>${poi.name}</b><br>
            ${poi.description || ''}<br>
            Type: ${poi.type}
          </div>
          <div style="display: flex; gap: 8px; justify-content: flex-end;">
            <button
              onclick="window.editPoi('${poi.identifier}')"
              style="padding: 4px 8px; background-color: #165dfc; color: white; border: none; border-radius: 4px; cursor: pointer; display: flex; align-items: center; gap: 4px;"
              title="Edit POI"
            >
              ✏️ Edit
            </button>
            <button
              onclick="window.deletePoi('${poi.identifier}')"
              style="padding: 4px 8px; background-color: #DC2626; color: white; border: none; border-radius: 4px; cursor: pointer; display: flex; align-items: center; gap: 4px;"
              title="Delete POI"
            >
              🗑️ Delete
            </button>
          </div>
        </div>
      `;

      marker.bindPopup(popupContent);
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

// Setup global functions for popup buttons
const setupGlobalHandlers = () => {
  (window as any).editPoi = (identifier: string) => {
    const poi = poisData.find(p => p.identifier === identifier);
    if (poi) {
      emit('edit-poi', poi);
    }
  };

  (window as any).deletePoi = (identifier: string) => {
    const poi = poisData.find(p => p.identifier === identifier);
    if (poi) {
      emit('delete-poi', identifier, poi.name);
    }
  };
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

  // Setup global handlers for popup buttons
  setupGlobalHandlers();

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


<template>
  <div class="map-container">
    <l-map
      :zoom="zoom"
      :center="center"
      :options="mapOptions"
      style="height: 100%"
    >
      <l-tile-layer
        :url="url"
        :attribution="attribution"
      />
    </l-map>
  </div>
</template>

<script lang="ts">
import { latLng } from "leaflet";
import { LMap, LTileLayer } from "vue2-leaflet";
import { Language } from '../models/Language';

export default {
  name: "VMap",
  components: {
    LMap,
    LTileLayer
  },
  data() {
    return {
      loading: true,
      showError: false,
      errorMessage: 'An unexpected error occured while loading the information to the map.',
      languages: [] as Language[],
      zoom: 3,
      center: latLng(60.166458996639314, 24.952770253219168),
      url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
      mapOptions: {
      }      
    };
  },
  methods: {
    async fetchLanguages() {
      try {
        const response = await this.$axios.get<Language>[]('api/Map');
        this.languages = response.data;
      } catch (e) {
        this.showError = true;
        this.errorMessage = `An unexpected error occured while loading the information to the map. The error message: ${e.message}`;
      }
      this.loading = false;
    },
    async created() {
      await this.fetchLanguages();
    }
  }
};
</script>

<!-- 
  Maybe one day I put these into separate file, but for now on the styles are here...
  https://vuejs.org/v2/style-guide/#Component-style-scoping-essential

  Component style coding / using the `scoped` attribute
-->
<style scoped>
    .map-container {
      height: 100%; 
      width: 100%;
    }

</style>

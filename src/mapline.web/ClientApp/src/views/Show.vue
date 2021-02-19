<!--
  Maybe one day I put these into separate file, but for now on the styles are here...
  https://vuejs.org/v2/style-guide/#Component-style-scoping-essential

  Component style coding / using the `scoped` attribute
-->
<style scoped>
  .show-map-container {
    height: 90%;
    width: 100%;
  }

  .filter-bar-component {
    display: inline-block;
    width: 8%;
    height: 100%;
    vertical-align: top;
  }

  .l-map {
    width: 90%;
    height: 100%;
    top: 0px;
    display: inline-block;
  }
</style>


<template>
  <div class="show-map-container">
    <map-control-player class="slider-component"
                        :current-year="mapControlPlayer.initialYear"
                        @on-year-update="onYearUpdate"
                        :min-year="mapControlPlayer.minYear"
                        :max-year="mapControlPlayer.maxYear"
                        :update-rate-in-milliseconds="mapControlPlayer.updateRateInMilliseconds"
                        :years-interval="mapControlPlayer.yearsInterval"
                        />

    <v-divider class="divider"></v-divider>

    <filter-bar class="filter-bar-component" />

    <v-divider class="divider" vertical></v-divider>

    <p v-if="loading">Loading...</p>
    <l-map class="l-map"
           ref="map"
           :zoom="zoom"
           :center="center"
           :options="mapOptions">
      <l-tile-layer :url="url"
                    :attribution="attribution" />

      <!--<l-geo-json v-for="feature in languagesGeoJson"
              :key="feature.index"
              :geojson="languagesGeoJson"
              :options="options"
              :options-style="styleFunction" />-->
      <l-geo-json 
                  :geojson="languagesGeoJson"
                  :options="options"
                  :options-style="styleFunction" />
    </l-map>
  </div>
</template>

<script lang="ts">
  import L from 'leaflet-draw'
  import { latLng } from "leaflet";
  import { LMap, LTileLayer, LGeoJson } from "vue2-leaflet";
  import { Language } from '../models/Language';
  import FilterBar from '@/components/FilterBar.vue' // @ is an alias to /src
  import MapControlPlayer from '@/components/MapControlPlayer.vue'

  export default {
    name: "VMap",
    components: {
      LMap,
      LTileLayer,
      LGeoJson,
      FilterBar,
      MapControlPlayer
    },
    data() {
      return {
        loading: true,
        show: true,
        languagesGeoJson: null,
        mapControlPlayer: {
          initialYear: -7000,
          onYearUpdate: "onYearUpdate",
          minYear: -7000,
          maxYear: 2021,
          updateRateInMilliseconds: 1000,
          yearsInterval: 5
        },
        polygonStyles: {
          hidden: {
            weight: 2,
            color: "rgba(0, 0, 0, 0)",
            opacity: 0,
            fillColor: "rgba(0, 0, 0, 0)",
            fillOpacity: 0,
          },
          visible: {
            weight: 2,
            color: "#ECEFF1", // Orange
            opacity: 1,
            fillColor: "#e4ce7f", // blue
            fillOpacity: 1
          }
        },
        errorMessage: 'An unexpected error occured while loading the information to the map.',
        languages: [] as Language[],
        zoom: 3,
        center: latLng(60.166458996639314, 24.952770253219168),
        url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
        mapOptions: {},
        marker: latLng(47.41322, -1.219482)
      };
    },

    methods: {
      onYearUpdate(currentYear) {
        for (var i in this.$refs.map.mapObject._layers) {
          if (this.$refs.map.mapObject._layers[i].feature === undefined)
            continue;

          var language = this.$refs.map.mapObject._layers[i].feature.properties;

          // The polygon is hidden by overriding styles
          if (language.yearStart <= currentYear && currentYear <= language.yearEnd) {
            // show polygon 
            this.$refs.map.mapObject._layers[i].feature.properties.show = true;
            this.$refs.map.mapObject._layers[i].setStyle(this.polygonStyles.visible);

          } else {
            // hide polygon 
            this.$refs.map.mapObject._layers[i].feature.properties.show = false;
            this.$refs.map.mapObject._layers[i].setStyle(this.polygonStyles.hidden);
          }
        } 
      }
    },

    computed: {
      options() { return { onEachFeature: this.onEachFeatureFucntion }; },
      styleFunction() { return this.polygonStyles.hidden; },

      onEachFeatureFunction() {
        if (!this.enableTooltip) {
          return;
        }

        return (feature, layer) => {
          layer.bindTooltip(`<div>code:${feature.properties.code}</div>`
            + `<div>nom: ${feature.properties.nom}</div>`,
            { permanent: false, sticky: true }
          );
        };
      },
    },

    async created() {
      try {
        this.loading = true;
        const response = await this.$axios.get<Language[]>('api/map/languages');

        this.languagesGeoJson = await response.data; 
        this.onYearUpdate(this.mapControlPlayer.initialYear);

        this.loading = false;
      } catch (e) {
        alert("An unexpected error occured in API handling...");
        console.log("An unexpected error occured in API handling. The error:");
        console.log(e);
      }
    },
  };
</script>
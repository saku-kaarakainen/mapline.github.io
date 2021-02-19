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
                        current-year="-7000"
                        @on-year-update="onYearUpdate"
                        min-year=-7000
                        max-year=2021
                        update-rate-in-milliseconds="1000"
                        years-interval="5" />

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
        hiddenLayers: [],
        errorMessage: 'An unexpected error occured while loading the information to the map.',
        languages: [] as Language[],
        zoom: 3,
        center: latLng(60.166458996639314, 24.952770253219168),
        url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
        mapOptions: {},
        fillColor: "#e4ce7f", // blue
        marker: latLng(47.41322, -1.219482)
      };
    },

    methods: {
      findFeaturePropertiesById(id) {
        var language = undefined;
        for (var i in this.languagesGeoJson.features) {
          var feature = this.languagesGeoJson.features[i];

          if (feature.properties.identifier == id)
            return feature.properties;

        }

        return language;
      },

      onYearUpdate(currentYear) {
        var map = this.$refs.map.mapObject;

        //console.log("this.$refs.map.mapObject._layers:");
        //console.log(this.$refs.map.mapObject._layers);

        //update the data of geoJson
        for (var i in this.languagesGeoJson.features) {
          var language = this.languagesGeoJson.features[i].properties;       

          if (language.yearStart <= currentYear && currentYear <= language.yearEnd) {
            // add layer if not exists
            this.languagesGeoJson.features[i].properties.show = true;

          } else {
            // remove layer if exists
            this.languagesGeoJson.features[i].properties.show = true;
          }
        } 



        // show->show
        // show->hide
        console.log("layers:");
        for (var j in map._layers) {
          var layer = map._layers[j];
          if (layer.feature === undefined)
            continue;

          // jos ID matchaa, ne on sama
          // => siirrä ID poistettuihin, jos show = false
          var updated = this.findFeaturePropertiesById(layer.feature.properties.identifier);

          if (updated.show === false) {
            console.log("piiloon");
            this.hiddenLayers.push(layer);
            map.removeLayer(layer);
          }

          console.log(layer.feature.properties.show);
        }

        // hide->show
        // hide->hide
      }
    },

    computed: {
      options() {
        return { onEachFeature: this.onEachFeatureFucntion };
      },

      styleFunction() {
        const fillColor = this.fillColor; // important! need touch fillColor in computed for re-calculate when change fillColor
        return () => {
          return {
            weight: 2,
            color: "#ECEFF1", // Orange
            opacity: 1,
            fillColor: fillColor,
            fillOpacity: 1
          };
        };
      },

      onEachFeatureFunction() {
        if (!this.enableTooltip) {
          // return () => { };
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
        //var features = await response.data.features;
        //this.languagesGeoJson = [];

        //for (var i = 0; i < features.length; i++) {
        //  features[i].show = false;
        //  this.languagesGeoJson.push(features[i]);
        //}

        this.loading = false;
      } catch (e) {
        alert("An unexpected error occured in API handling...");
        console.log("An unexpected error occured in API handling. The error:");
        console.log(e);
      }
    },
  };
</script>
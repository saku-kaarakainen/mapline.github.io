<template>
    <div class="map-container">
        <div>
            <span v-if="loading">Loading...</span>
            <label for="checkbox">GeoJSON Visibility</label>
            <input id="checkbox"
                   v-model="show"
                   type="checkbox">
            <label for="checkboxTooltip">Enable tooltip</label>
            <input id="checkboxTooltip"
                   v-model="enableTooltip"
                   type="checkbox">
            <input v-model="fillColor"
                   type="color">
            <br>
        </div>

        <l-map :zoom="zoom"
               :center="center"
               :options="mapOptions"
               style="height: 100%">
            <l-tile-layer :url="url"
                          :attribution="attribution" />

            <l-geo-json :geojson="languagesGeoJson"
                        :options="options"
                        :options-style="styleFunction" />

            <l-marker :lat-lng="marker" />

        </l-map>
    </div>
</template>

<script lang="ts">
    import { latLng } from "leaflet";
    import { LMap, LTileLayer, LMarker, LGeoJson } from "vue2-leaflet";
    import { Language } from '../models/Language';

    export default {
        name: "VMap",
        components: {
            LMap,
            LTileLayer,
            LMarker,
            LGeoJson
        },
        data() {
            return {
                loading: true,
                showError: false,
                show: true,
                enableTooltip: true,
                languagesGeoJson: null,
                errorMessage: 'An unexpected error occured while loading the information to the map.',
                languages: [] as Language[],
                zoom: 3,
                center: latLng(60.166458996639314, 24.952770253219168),
                url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
                mapOptions: {},
                fillColor: "#e4ce7f",
                marker: latLng(47.41322, -1.219482)
            };
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
                        color: "#ECEFF1",
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
            this.loading = true;
            const response = await this.$axios.get<Language[]>('api/Map')

            const data = await response.data;


            console.log("async create. data received:");
            console.log(data);

            this.languagesGeoJson = data[0].area;
            this.loading = false;
        },

        // async fetchLanguages() {
        //   try {
        //     const response = await this.$axios.get<Language>[]('api/Map');
        //     console.log("response.data:");
        //     console.log(response.data);
        //     this.languagesGeoJson = response.data;
        //   } catch (e) {
        //     this.showError = true;
        //     this.errorMessage = `An unexpected error occured while loading the information to the map. The error message: ${e.message}`;
        //   }
        //   this.loading = false;
        // },
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

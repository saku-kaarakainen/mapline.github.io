<style scoped>
    #map-control-editor-template {
        padding-left: 10px;
        padding-right: 10px;
    }

    #ranged-slider {
        width: 100%;
    }

    .map-control-buttons {
        margin-top: 10px;
    }
</style>
<template>
    <v-container id="map-control-editor-template">
        <v-row class="row-1">
            <v-btn 
            <v-text-field class="ma-2" :label="resources.yearStartHeader" v-model="local.yearRange[0]" />
            <v-text-field class="ma-2" :label="resources.yearEndHeader" v-model="local.yearRange[1]" />
        </v-row>

        <v-row class="row-2" md="1">
            <v-range-slider id="ranged-slider"
                      v-model="local.yearRange"
                      :min="scaleMin"
                      :max="scaleMax" />
        </v-row>
    </v-container>
</template>


<script lang="ts">
    // Sadly I don't master typescript, so I just write plain js...

    export default {
        name: 'map-control-editor',
        components: { },
        props: {
            scaleMin: { type: Number, default: -10000 },
            scaleMax: { type: Number, default: 2021 }
        },

        data() {
            return {
                resources: {
                    yearStartHeader: "Start Year:",
                    yearEndHeader: "End Year:"
                },

                local: { yearRange: [ this.scaleMin, this.scaleMax ], }
            };
        },

        async created() {
            try {
                console.log("editor created");

            } catch (e) {
                let message = `An unexpected error occuurred in components/MapControlEditor.vue/async created.`;
                alert(message);
                console.log(`${message} The error:`);
                console.log(e);
            }
        },

        //methods: {
        //},

        //beforeDestroy() {

        //}
    }
</script>
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
            <v-text-field class="ma-2" :label="resources.yearStartHeader" v-model="local.currentYear" />
            <v-text-field class="ma-2" :label="resources.yearEndHeader" v-model="local.currentYear" />
        </v-row>

        <v-row class="row-2" md="1">
            <v-range-slider id="ranged-slider"
                      v-model="local.yearRange"
                      :min="local.scale.min"
                      :max="local.scale.max" />
        </v-row>
    </v-container>
</template>

<script lang="ts">

    export default {
        name: 'map-control-editor',
        components: {

        },

        props: {
            startYear: { type: Number },
            endYear: { type: Number },
            scaleMin: {
                type: Number,
                default: -10000
            },
            scaleMax: {
                type: Number,
                default: 2021
            }
        },

        data() {
            return {
                // local -object
                // https://forum.vuejs.org/t/naming-practices-for-private-getter-variables/13905

                resources: {
                    yearStartHeader: "Start Year:",
                    yearEndHeader: "End Year:"
                },

                local: {
                    scale: {
                        min: this.scaleMin.value,
                        max: this.scaleMax.value,
                    },

                    yearRange: [
                        -2000,
                        1500
                    ],


                    // These should be matched with props: { }
                    startYear: this.currentYear,
                }
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
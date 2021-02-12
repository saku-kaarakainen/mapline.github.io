<style scoped>
    #map-control-template {
        padding-left: 10px;
        padding-right: 10px;
    }

    #ranged-slider {
        width: 100%;
    }

    div.col button {
        margin-top: 10px;
    }

</style>
<template>
    <v-container id="map-control-template">
        <v-row class="row-1">

            <v-text-field class="ma-2" :label="resources.yearHeader" v-model="currentYear" />

            <v-btn class="ma-2" @click="playOrPause">
                <div class="playing" v-if="isPlaying">
                    <v-icon left>mdi-pause</v-icon>
                    Pause
                </div>
                <div class="at-pause" v-else>
                    <v-icon left>mdi-play</v-icon>
                    Play
                </div>
            </v-btn>

            <v-btn class="ma-2" @click="revertDirection">
                <v-icon v-if="isDirectionToRight">mdi-arrow-right</v-icon>
                <v-icon v-else>mdi-arrow-left</v-icon>
            </v-btn>

            <v-text-field class="ma-2" :label="resources.intervalHeader" v-model="stepsPerInterval" />

            <v-text-field class="ma-2" :label="resources.updateRateHeader" v-model="updateRateInMilliseconds" />

        </v-row>

        <v-row class="row-2" md="1">
            <v-slider id="ranged-slider"
                      v-model="currentYear"
                      :min="min"
                      :max="max" />
        </v-row>
    </v-container>
</template>

<script lang="ts">

    export default {
        name: 'map-control',       
        components: {
            
        },

        data() {
            return {
                resources: {
                    yearHeader: "Current Year:",
                    intervalHeader: "Interval (years):",
                    updateRateHeader: "Updates every (ms):"
                },
                isPlaying: true,
                isDirectionToRight: true,
                currentYear: -7000, // -7000,
                timer: '',
                min: -7000,
                max: 2021,
                updateRateInMilliseconds: 1000,
                stepsPerInterval: 5
            };
        },
        
        async created() {
            try {
                // updates every second
                this.timer = setInterval(this.updateTimer, this.updateRateInMilliseconds);
            } catch (e) {
                alert("An unexpected error occuurred in components/slider.vue/async created.");
                console.log("An unexpected error occuurred in components/slider.vue/async created. The error:");
                console.log(e);
            }
        },

        methods: {
            updateTimer: function () {
                if (this.isPlaying) {

                    if (this.isDirectionToRight)
                        this.currentYear += this.stepsPerInterval;
                    else
                        this.currentYear -= this.stepsPerInterval;
                }
            },
            cancelAutoUpdate() { clearInterval(this.timer); },
            playOrPause: function () { this.isPlaying = !this.isPlaying; },
            revertDirection: function () { this.isDirectionToRight = !this.isDirectionToRight; }
        },

        beforeDestroy() {
            clearInterval(this.timer);
        }
    }
</script>
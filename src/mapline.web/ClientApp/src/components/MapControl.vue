<style scoped>
    #map-control-template {
        padding-left: 10px;
        padding-right: 10px;
    }

    #ranged-slider {
        width: 100%;
    }

    input, .bordered {
        border: solid 1px black;
    }

</style>
<template>
    <div id="map-control-template">
        
        <div class="row-1">

            <!-- shown year -->
            <span class="bordered">
                <label id="slider-header" for="ranged-slider">{{ resources.yearHeader }}</label>
                <span id="current-value">{{ currentYear }}</span>
            </span>

            <!-- 'animation' controls -->
            <v-btn type="submit" value="">
                <v-icon left>{{ icons.mdiPlay }}</v-icon>
                Play/Pause
            </v-btn>


            <input type="submit" value="Revert Direction" />

            <!-- configurtations -->
            <span class="bordered">
                <label id="slider-header" for="ranged-slider">{{ resources.intervalHeader }}</label>
                <span id="current-value">{{ stepsPerInterval }}</span>
            </span>

            <span class="bordered">
                <label id="slider-header" for="ranged-slider">{{ resources.updateRateHeader }}</label>
                <span id="current-value">{{ updateRateInMilliseconds }}</span>
            </span>

        </div>

        <div class="row-2">
            <v-slider id="ranged-slider"
                    v-model="currentYear"
                    :min="min"
                    :max="max"
            />
        </div>
   
    </div>
</template>

<script lang="ts">
    import { mdiPlay, mdiPause } from '@mdi/js'

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

                icons: { mdiPlay, mdiPause },

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
                this.currentYear += this.stepsPerInterval;
            },
            cancelAutoUpdate() {
                clearInterval(this.timer);
            }
        },

        beforeDestroy() {
            clearInterval(this.timer);
        }
    }
</script>
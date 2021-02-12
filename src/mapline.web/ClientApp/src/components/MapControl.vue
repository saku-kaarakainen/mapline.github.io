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

            <!-- play / pause -->
            <v-btn @click="playOrPause">
                <div class="playing" v-if="isPlaying">
                    <v-icon left>mdi-pause</v-icon>
                    Pause
                </div>
                <div class="at-pause" v-else>
                    <v-icon left>mdi-play</v-icon>
                    Play
                </div>
            </v-btn>


            <!--<input type="submit" value="Revert Direction" />-->
            <v-btn @click="revertDirection">
                <v-icon v-if="isDirectionToRight">mdi-arrow-right</v-icon> 
                <v-icon v-else>mdi-arrow-left</v-icon>
            </v-btn>

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
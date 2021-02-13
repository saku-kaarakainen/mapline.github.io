<style scoped>
    #map-control-player-template {
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
    <v-container id="map-control-player-template">
        <v-row class="row-1">

            <v-text-field class="ma-2" :label="resources.yearHeader" v-model="pCurrentYear" />

            <div class="map-control-player-buttons">
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

                <v-btn class="ma-2 map-control-player-button" @click="revertDirection">
                    <v-icon v-if="isDirectionToRight">mdi-arrow-right</v-icon>
                    <v-icon v-else>mdi-arrow-left</v-icon>
                </v-btn>
            </div>

            <v-text-field class="ma-2" :label="resources.intervalHeader" v-model="pYearsInterval" />

            <v-text-field class="ma-2" :label="resources.updateRateHeader" v-model="pUpdateRateInMilliseconds" />

        </v-row>

        <v-row class="row-2" md="1">
            <v-slider id="ranged-slider"
                      v-model="pCurrentYear"
                      :min="pMinYear"
                      :max="pMaxYear" />
        </v-row>
    </v-container>
</template>

<script lang="ts">

    export default {
        name: 'map-control-player',       
        components: {
            
        },

        props: {
            currentYear: Number,
            minYear: Number,
            maxYear: Number,

            updateRateInMilliseconds: {
                type: Number,
                default: 1000 // 1 second
            },

            yearsInterval: {
                type: Number,
                default: 1 // years
            }
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
                timer: '',

                // private fields
                pCurrentYear: this.currentYear,
                pMinYear: this.minYear,
                pMaxYear: this.maxYear,
                pUpdateRateInMilliseconds: this.updateRateInMilliseconds,
                pYearsInterval: this.yearsInterval
            };
        },
        
        async created() {
            try {
                // updates every second
                this.timer = setInterval(this.updateTimer, this.pUpdateRateInMilliseconds);
            } catch (e) {
                let message = `An unexpected error occuurred in components/MapControlPlayer.vue/async created.`;
                alert(message);
                console.log(`${message} The error:`);
                console.log(e);
            }
        },

        methods: {
            updateTimer: function () {   
                if (this.isPlaying) {

                    if (this.isDirectionToRight)
                        this.pCurrentYear += this.pYearsInterval;
                    else
                        this.pCurrentYear -= this.pYearsInterval;
                }

                this.$emit("update-year", this.pCurrentYear);
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
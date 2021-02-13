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

            <v-text-field class="ma-2" :label="resources.yearHeader" v-model="local.currentYear" />

            <div class="map-control-player-buttons">
                <v-btn class="ma-2" @click="playOrPause">
                    <div class="playing" v-if="local.isPlaying">
                        <v-icon left>mdi-pause</v-icon>
                        Pause
                    </div>
                    <div class="at-pause" v-else>
                        <v-icon left>mdi-play</v-icon>
                        Play
                    </div>
                </v-btn>

                <v-btn class="ma-2 map-control-player-button" @click="revertDirection">
                    <v-icon v-if="local.isDirectionToRight">mdi-arrow-right</v-icon>
                    <v-icon v-else>mdi-arrow-left</v-icon>
                </v-btn>
            </div>

            <v-text-field class="ma-2" :label="resources.intervalHeader" v-model="local.yearsInterval" />

            <v-text-field class="ma-2" :label="resources.updateRateHeader" v-model="local.updateRateInMilliseconds" />

        </v-row>

        <v-row class="row-2" md="1">
            <v-slider id="ranged-slider"
                      v-model="local.currentYear"
                      :min="local.minYear"
                      :max="local.maxYear" />
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
                // local -object
                // https://forum.vuejs.org/t/naming-practices-for-private-getter-variables/13905

                resources: {
                    yearHeader: "Current Year:",
                    intervalHeader: "Interval (years):",
                    updateRateHeader: "Updates every (ms):"
                },

                local: {
                    isPlaying: true,
                    isDirectionToRight: true,
                    timer: '',

                    // These should be matched with props: { }
                    currentYear: this.currentYear,
                    minYear: this.minYear,
                    maxYear: this.maxYear,
                    updateRateInMilliseconds: this.updateRateInMilliseconds,
                    yearsInterval: this.yearsInterval
                }
            };
        },
        
        async created() {
            try {
                this.local.timer = setInterval(this.updateTimer, this.local.updateRateInMilliseconds);
            } catch (e) {
                let message = `An unexpected error occuurred in components/MapControlPlayer.vue/async created.`;
                alert(message);
                console.log(`${message} The error:`);
                console.log(e);
            }
        },

        methods: {
            updateTimer: function () {   
                if (this.local.isPlaying) {

                    if (this.local.isDirectionToRight)
                        this.local.currentYear += this.local.yearsInterval;
                    else
                        this.local.currentYear -= this.local.yearsInterval;
                }

                this.$emit("update-year", this.local.currentYear);
            },
            cancelAutoUpdate() { clearInterval(this.local.timer); },
            playOrPause: function () { this.local.isPlaying = !this.local.isPlaying; },
            revertDirection: function () { this.local.isDirectionToRight = !this.local.isDirectionToRight; }
        },

        beforeDestroy() {
            clearInterval(this.local.timer);
        }
    }
</script>
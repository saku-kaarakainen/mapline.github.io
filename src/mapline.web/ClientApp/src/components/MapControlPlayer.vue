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

      <v-text-field class="ma-2" :label="resources.yearHeader" v-model="local_currentYear" />

      <div class="map-control-player-buttons">
        <v-btn class="ma-2" @click="playOrPause">
          <div class="playing" v-if="local_isPlaying">
            <v-icon left>mdi-pause</v-icon>
            Pause
          </div>
          <div class="at-pause" v-else>
            <v-icon left>mdi-play</v-icon>
            Play
          </div>
        </v-btn>

        <v-btn class="ma-2 map-control-player-button" @click="revertDirection">
          <v-icon v-if="local_isDirectionToRight">mdi-arrow-right</v-icon>
          <v-icon v-else>mdi-arrow-left</v-icon>
        </v-btn>
      </div>

      <v-text-field class="ma-2" :label="resources.intervalHeader" v-model="local_yearsInterval" />

      <v-text-field class="ma-2" :label="resources.updateRateHeader" v-model="local_updateRateInMilliseconds" v-on:change="updateInterval" />

    </v-row>

    <v-row class="row-2" md="1">
      <v-slider id="ranged-slider"
                v-model="local_currentYear"
                :min="local_minYear"
                :max="local_maxYear" />
    </v-row>
  </v-container>
</template>

<script lang="ts">
  import { Action, Getter, Mutation } from 'vuex-class'
  import { Component, Vue, Prop } from 'vue-property-decorator'

  class PlayerResources {
    yearHeader: string
    intervalHeader: string
    updateRateHeader: string
  }

  @Component
  export default class Player extends Vue {
    async created() {
      try {
        this.updateInterval();
      } catch (e) {
        let message = `An unexpected error occuurred in components/MapControlPlayer.vue/async created.`;
        alert(message);
        console.log(`${message} The error:`);
        console.log(e);
      }
    }


    constructor() {
      super()

      this.resources = new PlayerResources();
      this.resources.yearHeader = "Current Year:",
      this.resources.intervalHeader = "Interval (years):",
      this.resources.updateRateHeader = "Updates every (ms):"

      this.local_isPlaying = true
      this.local_isDirectionToRight = true
      this.local_timer = undefined
    }

    readonly resources: PlayerResources

    @Prop({})   
    public currentYear: number
    private local_currentYear: number = this.currentYear;

    @Prop()
    public minYear: number
    private local_minYear: number = this.minYear;

    @Prop()
    public maxYear: number
    private local_maxYear: number = this.maxYear;

    @Prop({ default: 1000 })
    public updateRateInMilliseconds: number
    private local_updateRateInMilliseconds: number = this.updateRateInMilliseconds;

    @Prop({ default: 1 })
    public yearsInterval: number
    private local_yearsInterval: number = this.yearsInterval;

    private local_isPlaying: boolean
    private local_isDirectionToRight: boolean
    private local_timer: number


    private updateTimer(): void {
      var interval = parseInt(this.local_yearsInterval);
      var year = parseInt(this.local_currentYear);

      if (this.local_isDirectionToRight)
        year += interval;
      else 
        year -= interval;      

      this.local_currentYear = year;
      this.$emit("on-year-update", this.local_currentYear);
    }

    private cancelAutoUpdate(): void {
      clearInterval(this.local_timer);
    }

    private playOrPause(): void {    
      this.local_isPlaying = !this.local_isPlaying;
      this.updateInterval();
    }

    private updateInterval(): void {
      clearInterval(this.local_timer);

      if (this.local_isPlaying) {
        this.local_timer = setInterval(this.updateTimer, this.local_updateRateInMilliseconds);
      }
    }

    private revertDirection(): void {
      this.local_isDirectionToRight = !this.local_isDirectionToRight;
    }

    beforeDestroy(): void {
      clearInterval(this.local_timer);
    }
  }
</script>
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

      <v-text-field class="ma-2" :label="resources.yearHeader" v-model="_currentYear" />

      <div class="map-control-player-buttons">
        <v-btn class="ma-2" @click="_playOrPause">
          <div class="playing" v-if="_isPlaying">
            <v-icon left>mdi-pause</v-icon>
            Pause
          </div>
          <div class="at-pause" v-else>
            <v-icon left>mdi-play</v-icon>
            Play
          </div>
        </v-btn>

        <v-btn class="ma-2 map-control-player-button" @click="revertDirection">
          <v-icon v-if="_isDirectionToRight">mdi-arrow-right</v-icon>
          <v-icon v-else>mdi-arrow-left</v-icon>
        </v-btn>
      </div>

      <v-text-field class="ma-2" :label="resources.intervalHeader" v-model="_yearsInterval" />

      <v-text-field class="ma-2" :label="resources.updateRateHeader" v-model="_updateRateInMilliseconds" />

    </v-row>

    <v-row class="row-2" md="1">
      <v-slider id="ranged-slider"
                v-model="_currentYear"
                :min="_minYear"
                :max="_maxYear" />
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
        this.localtimer = setInterval(this.localupdateTimer, this.localupdateRateInMilliseconds);
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

      this.localisPlaying = true
      this.localisDirectionToRight = true
      this.localtimer = undefined
    }

    readonly resources: PlayerResources

    @Prop({})   
    public currentYear: number
    private _currentYear = this.currentYear;

    @Prop()
    public minYear: number
    private _minYear = this.minYear;

    @Prop()
    public maxYear: number
    private _maxYear = this.maxYear;

    @Prop({ default: 1000 })
    public updateRateInMilliseconds: number
    private _updateRateInMilliseconds = this.updateRateInMilliseconds;

    @Prop({ default: 1 })
    public yearsInterval: number
    private _yearsInterval = this.yearsInterval;

    private _isPlaying: boolean
    private _isDirectionToRight: boolean
    private _timer: number

    private updateTimer(): void {
      if (this.localisPlaying) {
        if (this.localisDirectionToRight)
          this.localcurrentYear += this.localyearsInterval;
        else
          this.localcurrentYear -= this.localyearsInterval;
      }

      this.$emit("on-year-update", this.localcurrentYear);
    }

    private cancelAutoUpdate(): void {
      clearInterval(this.localtimer);
    }

    private playOrPause(): void {
      this.localisPlaying = !this.localisPlaying;
    }

    private revertDirection(): void {
      this.localisDirectionToRight = !this.localisDirectionToRight;
    }

    beforeDestroy(): void {
      clearInterval(this.localtimer);
    }
  }
</script>
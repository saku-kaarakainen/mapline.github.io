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

      <v-text-field class="ma-2" :label="resources.yearHeader" v-model="currentYear" />

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

      <v-text-field class="ma-2" :label="resources.intervalHeader" v-model="yearsInterval" />

      <v-text-field class="ma-2" :label="resources.updateRateHeader" v-model="updateRateInMilliseconds" />

    </v-row>

    <v-row class="row-2" md="1">
      <v-slider id="ranged-slider"
                v-model="currentYear"
                :min="minYear"
                :max="maxYear" />
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
  export default class MapControlPlayer extends Vue {
    async created() {
      try {
        this.timer = setInterval(this.updateTimer, this.updateRateInMilliseconds);
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

      this.isPlaying = true
      this.isDirectionToRight = true
      this.timer = undefined
    }

    readonly resources: PlayerResources

    @Prop() public currentYear: number
    @Prop() public minYear: number
    @Prop() public maxYear: number
    @Prop({ default: 1000 }) public updateRateInMilliseconds: number
    @Prop({ default: 1 }) public yearsInterval: number

    private isPlaying: boolean
    private isDirectionToRight: boolean
    private timer: number

    private updateTimer(): void {
      if (this.isPlaying) {
        if (this.isDirectionToRight)
          this.currentYear += this.yearsInterval;
        else
          this.currentYear -= this.yearsInterval;
      }

      this.$emit("update-year", this.currentYear);
    }

    private cancelAutoUpdate(): void {
      clearInterval(this.timer);
    }

    private playOrPause(): void {
      this.isPlaying = !this.isPlaying;
    }

    private revertDirection(): void {
      this.isDirectionToRight = !this.isDirectionToRight;
    }

    beforeDestroy(): void {
      clearInterval(this.timer);
    }
  }

  //export default {
  //  name: 'map-control-player',
  //  components: {
  //  }
  //  
  //  
  //  
  //  
  //  
  //  
  //  
  //  
  //  
  //  

  //}
</script>
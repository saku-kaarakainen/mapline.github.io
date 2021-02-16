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
     <v-btn type="submit" @click.prevent="increment">Save to file</v-btn>
    <!--<v-row class="row-1">
      <div class="col-md-1">
        <v-btn type="submit" v-on:click="add">{ { resources.save } }</v-btn>
      </div>
      <div class="col-md-11">
        <v-text-field class="ma-2" :label="resources.yearStartHeader" v-model="local.yearRange[0]" />
        <v-text-field class="ma-2" :label="resources.yearEndHeader" v-model="local.yearRange[1]" />
      </div>
    </v-row>

    <v-row class="row-2" md="1">
      <v-range-slider id="ranged-slider"
                      v-model="local.yearRange"
                      :min="scaleMin"
                      :max="scaleMax" />
    </v-row>-->
  </v-container>
</template>


<script lang="ts">
  import { Action, Getter } from 'vuex-class'
  import { Component, Vue } from 'vue-property-decorator'
  const namespace = 'editor'

  @Component
  export default class Editor extends Vue {
    @Getter('currentCount', { namespace })
    private currentCount!: number

    @Action('increment', { namespace })
    private incrementEditor!: () => void

    @Action('reset', { namespace })
    private resetEditor!: () => void

    private increment() {
      console.log("private increment");

      this.incrementEditor()
    }

    private reset() {
      console.log("private reset")

      this.resetEditor()
    }
  }

  //// Sadly I don't master typescript, so I just write plain js...
  //export default {
  //  name: 'map-control-editor',
  //  components: {},
  //  props: {
  //    scaleMin: { type: Number, default: -10000 },
  //    scaleMax: { type: Number, default: 2021 }
  //  },

  //  data() {
  //    return {
  //      resources: {
  //        save: "Save",
  //        yearStartHeader: "Start Year:",
  //        yearEndHeader: "End Year:"
  //      },

  //      local: { yearRange: [this.scaleMin, this.scaleMax], }
  //    };
  //  },

  //  async created() {
  //    try {
  //      console.log("editor created");

  //    } catch (e) {
  //      let message = `An unexpected error occuurred in components/MapControlEditor.vue/async created.`;
  //      alert(message);
  //      console.log(`${message} The error:`);
  //      console.log(e);
  //    }
  //  },

  //  methods: {
  //    add() {
  //      var editorData = {
  //        yearStart: this.local.yearRange[0],
  //        yearEnd: this.local.yearRange[1]
  //      };

  //      this.$emit('add', editorData);
  //    }
  //  },

  //  //beforeDestroy() {

  //  //}
  //}
</script>
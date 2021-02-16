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
     <!--<v-btn type="submit" @click.prevent="add">Save</v-btn>-->
    <v-row class="row-1">
      <div class="col-md-1">
        <v-btn type="submit" @click.prevent="add">Save</v-btn>
      </div>
      <div class="col-md-11">
        <v-text-field class="ma-2" v-model="currentLanguage.yearRange[0]" @input="update" />
        <v-text-field class="ma-2" v-model="currentLanguage.yearRange[1]" @input="update" />
      </div>
    </v-row>

    <v-row class="row-2" md="1">
      <v-range-slider id="ranged-slider"
                      @input="update"
                      v-model="currentLanguage.yearRange"
                      :min="-10000"
                      :max="2021" />
    </v-row>
  </v-container>
</template>


<script lang="ts">
  import { Action, Getter, Mutation } from 'vuex-class'
  import { Component, Vue } from 'vue-property-decorator'
  import { Language } from '@/store/editor/types'
  const namespace = 'editor'

  @Component
  export default class Editor extends Vue {
    @Getter('currentLanguage', { namespace })
    private currentLanguage!: Language

    @Mutation('updateLanguage', { namespace })
    private updateLanguage!: (value: Language) => void


    @Action('add', { namespace })
    private addLanguage!: () => void

    private add() {
      this.addLanguage();
    }

    public update(): void {
      this.updateLanguage(this.currentLanguage);
    }
  }

  // Sadly I don't master typescript, so I just write plain js...
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
  //    }
  //  },

  //  //beforeDestroy() {

  //  //}
  //}
</script>
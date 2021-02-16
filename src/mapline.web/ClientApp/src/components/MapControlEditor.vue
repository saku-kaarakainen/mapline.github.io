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
    <v-row class="row-1">
      <div class="col-md-1">
        <v-btn type="submit" @click.prevent="add">{{ resources.save }}</v-btn>
      </div>
      <div class="col-md-11">
        <v-text-field class="ma-1" :label="resources.name" v-model="currentLanguage.name" />
        <v-text-field class="ma-2" :label="resources.yearStart" v-model="currentLanguage.yearRange[0]" @input="update" />
        <v-text-field class="ma-2" :label="resources.yearEnd" v-model="currentLanguage.yearRange[1]" @input="update" />
      </div>
    </v-row>

    <v-row class="row-2" md="1">
      <v-range-slider id="ranged-slider"
                      @input="update"
                      v-model="currentLanguage.yearRange"
                      :min="scaleMin"
                      :max="scaleMax" />
    </v-row>
  </v-container>
</template>


<script lang="ts">
  import { Action, Getter, Mutation } from 'vuex-class'
  import { Component, Vue, Prop } from 'vue-property-decorator'
  import { Language } from '@/store/editor/types'
  const namespace = 'editor'

  // TODO: An actual resource editor
  class EditorResorces {
    save: string
    name: string
    yearStart: string
    yearEnd: string
  }

  @Component
  export default class Editor extends Vue {
    constructor() {
      super()
      this.resources = new EditorResorces();
      this.resources.save = "Save";
      this.resources.name = "Name";
      this.resources.yearStart = "Start Year";
      this.resources.yearEnd = "End Year";
    }

    readonly resources: EditorResorces

    @Prop({ default: -10000 })
    public scaleMin: number

    @Prop({ default: 2021 })
    public scaleMax: number

    @Getter('currentLanguage', { namespace })
    private currentLanguage!: Language

    @Mutation('updateEditorData', { namespace })
    private updateEditorData!: (value: Language) => void

    public update(): void {
      this.updateEditorData(this.currentLanguage);
    }

    public add(): void {
      console.log(this.currentLanguage.name);
      console.log(this.currentLanguage.area);
      console.log(this.currentLanguage.yearRange);
    }
  }
</script>
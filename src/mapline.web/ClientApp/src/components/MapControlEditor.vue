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
    <v-row class="row-0">
      <v-progress-circular v-show="loadingIntoDb"
                           indeterminate
                           color="primary">
      </v-progress-circular>
      <v-alert type="success" v-show="addedToDb">Data added to the database</v-alert>
      <v-alert type="error" v-show="addFailedToDb">Unable to add the data to the database</v-alert>

      <v-progress-circular v-show="loadingIntoFile"
                           indeterminate
                           color="primary">
      </v-progress-circular>
      <v-alert type="success" v-show="addedToFile">Data added to File</v-alert>
      <v-alert type="error" v-show="addFailedToFile">Unable to add the data to the file</v-alert>
    </v-row>


    <v-row class="row-1">
      <div class="col-md-1">
        <v-btn type="submit" @click.prevent="saveToDb">Add to DB</v-btn>
        <br />
        <v-btn type="submit" @click.prevent="saveToFile">Add to File</v-btn>
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
  import { SaveLanguage } from '../models/SaveLanguage'
  const namespace = 'editor'

  // TODO: An actual resource editor
  class EditorResorces {
    name: string
    yearStart: string
    yearEnd: string
  }

  @Component
  export default class Editor extends Vue {
    constructor() {
      super()
      this.resources = new EditorResorces();
      this.resources.name = "Name";
      this.resources.yearStart = "Start Year";
      this.resources.yearEnd = "End Year";

      this.loadingIntoDb = false;
      this.loadingIntoFile = false;    
      this.addedToDb = false;
      this.addedToFile = false;
      this.addFailedToDb = false;
      this.addFailedToFile = false;
    }

    readonly resources: EditorResorces
    private loadingIntoDb: boolean
    private loadingIntoFile: boolean    
    private addedToDb: boolean
    private addedToFile: boolean   
    private addFailedToDb: boolean
    private addFailedToFile: boolean

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

    public async saveToDb(): Promise<void> {
      try {
        this.loadingIntoDb = true;
        await this.save('api/administrator/save/db');
        this.loadingIntoDb = false;
        this.addedToDb = true;
      } catch (e) {
        this.addFailedToDb = true;
        var message = "An error occurred while adding the map data.";
        alert(message);
        console.log(`${message} The error:`);
        console.log(e);
      }
    }

    public async saveToFile(): Promise<void> {
      try {
        this.loadingIntoFile = true;
        await this.save('api/administrator/save/file');
        this.loadingIntoFile = false;
        this.addedToFile = true;
      } catch (e) {
        this.addFailedToFile = true;
        var message = "An error occurred while adding the map data.";
        alert(message);
        console.log(`${message} The error:`);
        console.log(e);
      }
    }

    public async save(url: string): Promise<void> {
      if (!this.validate()) {
        alert("Check your input");
        return;
      }

      var language = {
        Name: this.currentLanguage.name,
        GeoJson: this.currentLanguage.area,
        YearStart: this.currentLanguage.yearRange[0],
        YearEnd: this.currentLanguage.yearRange[1],
      };

        await this.$axios.post<SaveLanguage>(url, language)
    }

    private validate(): boolean {
      // name
      if (!this.currentLanguage.name) {
        console.log("validate: invalid name")
        return false;
      }
      // area
      if (!this.currentLanguage.area) {
        console.log("validate: invalid area")
        return false;
      }
      // year range
      if (!this.currentLanguage.yearRange
        || this.currentLanguage.yearRange.length != 2
        || !this.currentLanguage.yearRange[0]
        || !this.currentLanguage.yearRange[1]
      ) {
        console.log("validate: invalid years")
        return false;
      }
      return true;
    }
  }
</script>
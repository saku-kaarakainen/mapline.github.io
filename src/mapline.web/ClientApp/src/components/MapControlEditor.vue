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
      <v-progress-circular v-show="loading" indeterminate color="primary"></v-progress-circular>
    </v-row>


    <v-row class="row-1">
      <div class="col-md-1">
        <v-btn type="submit" @click.prevent="saveToDb">Add to DB</v-btn>
        <br />
        <v-btn type="submit" @click.prevent="saveToFile">Add to File</v-btn>
      </div>
      <div class="col-md-11">
        <v-text-field class="ma-1" :label="resources.name" v-model="currentLanguage.name" />
        
        <!-- these components seems to crash if the v-model value is only "-". -->
        <v-text-field class="ma-2" :label="resources.yearStart" type="number" v-model="local_yearRange[0]" @input="update" />
        <v-text-field class="ma-2" :label="resources.yearEnd" type="number" v-model="local_yearRange[1]" @input="update" />
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
  import store from '@/store'
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

      this.loading = false;

      this.local_yearRange = new Array<number>(0, 0);
    }

    readonly resources: EditorResorces
    private loading: boolean  

    @Prop({ default: -10000 })
    public scaleMin: number

    @Prop({ default: 2021 })
    public scaleMax: number

    @Getter('currentLanguage', { namespace })
    private currentLanguage!: Language    

    private local_yearRange: Array<number>;

    @Mutation('updateEditorData', { namespace })
    private updateEditorData!: (value: Language) => void

    public update(): void {
      for (var i in this.local_yearRange) {
        if (isNaN(this.local_yearRange[i])) {
          this.local_yearRange[i] = 0;
        }
      }

      this.currentLanguage.yearRange = this.local_yearRange;
      this.updateEditorData(this.currentLanguage);
    }

    public async saveToDb(): Promise<void> {
      try {
        this.loading = true;
        await this.save('api/administrator/save/db');
        this.loading = false;

        alert("Data added to the database");
      } catch (e) {
        var message = "An error occurred while adding the map data.";
        alert(message);
        console.log(`${message} The error:`);
        console.log(e);
      }
    }

    public async saveToFile(): Promise<void> {
      try {
        this.loading = true;
        await this.save('api/administrator/save/file');
        this.loading = false;

        alert("Data added to the file");
      } catch (e) {
        var message = "An error occurred while adding the map data.";
        alert(message);
        console.log(`${message} The error:`);
        console.log(e);
      }
    }

    public async save(url: string): Promise<void> {
      this.throwIfInvalid(); 

      var language = {
        Name: this.currentLanguage.name,
        GeoJson: this.currentLanguage.area,
        YearStart: this.currentLanguage.yearRange[0],
        YearEnd: this.currentLanguage.yearRange[1],
      };

        await this.$axios.post<SaveLanguage>(url, language)
    }

    private throwIfInvalid(): boolean {
      // name
      if (!this.currentLanguage.name) 
        throw "Validation failed: invalid name";
      
      // area
      if (!this.currentLanguage.area) 
        throw "Validation failed: invalid area";
      
      // year range
      if (!this.currentLanguage.yearRange
        || this.currentLanguage.yearRange.length != 2
        || isNaN(this.currentLanguage.yearRange[0])
        || isNaN(this.currentLanguage.yearRange[1])) 
        throw "Validation failed: invalid years";
      
    }
  }
</script>
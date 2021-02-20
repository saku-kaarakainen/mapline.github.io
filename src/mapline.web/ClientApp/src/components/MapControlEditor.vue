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
        <v-btn type="submit" @click.prevent="saveToDb">{{ resources.addToDb }}</v-btn>
        <br />
        <v-btn type="submit" @click.prevent="saveToFile">{{ resources.addToFile }}</v-btn>
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
  import { mapActions, mapGetters, mapMutations } from 'vuex'
  import { Action, Getter, Mutation } from 'vuex-class'
  import { Component, Vue, Prop } from 'vue-property-decorator'
  import { Language } from '@/store/editor/types'
  import store from '@/store'
  import { SaveLanguage } from '../models/SaveLanguage'

  const namespace = 'editor'

  export default {
    name: "map-control-editor",

    components: {},

    props: {
      scaleMin: { type: Number, default: -10000 },
      scaleMax: { type: Number, default: 2021 }
    },

    data() {
      return {
        loading: false,
        resources: {
          name: "Name",
          yearStart: "Start Year",
          yearEnd: "End Year",
          addToDb: "Add to DB",
          addToFile: "Add to file"
        }
      }
    },

    computed: {
      ...mapGetters({ currentLanguage: 'editor/currentLanguage' }),
      //...mapActions({ updateEditorData: 'editor/updateEditorData' }),
    },

    methods: {

      update(): void {
        // I don't know why ...mapActions with the param doesn't work. So i us more straighforward solution...
        //this.updateEditorData(this.currentLanguage);
        store.dispatch({
          type: 'editor/updateEditorData',
          language: this.currentLanguage
        });
      },

      async saveToDb() {
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
      },

      async saveToFile(): Promise<void> {
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
      },

      async save(url: string): Promise<void> {
        this.throwIfInvalid();

        console.log("save/this.local_currentLanguage");
        var language = {
          Name: this.local_currentLanguage_name,
          GeoJson: this.local_currentLanguage_area,
          YearStart: this.local_currentLanguage_yearStart,
          YearEnd: this.local_currentLanguage_yearEnd,
        };

        await this.$axios.post<SaveLanguage>(url, language)
      },

      throwIfInvalid(): boolean {
        // name
        if (!this.local_currentLanguage_name)
          throw "Validation failed: invalid name";

        // area
        if (!this.local_currentLanguage_area)
          throw "Validation failed: invalid area";

        // year range
        //if (!this.local_currentLanguage_yearRange
        //  || this.local_currentLanguage.yearRange.length != 2
        //  || this.local_currentLanguage.yearRange[0] === undefined
        //  || this.local_currentLanguage.yearRange[1] === undefined)
        //  throw "Validation failed: invalid years";

      }
    }
  }
</script>
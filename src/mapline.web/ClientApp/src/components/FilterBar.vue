<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

<template>
  <div id="filter-bar-template">
    <p>{{ headerText }}</p>
    <div v-for="(item, index) in filters" :key="index">
      <input type="checkbox" :id="item.name" v-model="item.checked" @change="filterChange($event)" />
      <label :for="item.name">{{ item.name }}</label>
    </div>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        headerText: "Filters:",
        filters: []
      };
    },

    name: 'filter-bar',

    methods: {
      filtersChanged: function (e) {
        //console.log(this.filters, e);
      }
    },

    async created() {
      try {
        this.filters = (await this.$axios.get('api/map/filters')).data;
      } catch (e) {
        alert("An unexpected error occured in API call `api/map/filters`...")
        console.log("An unexpected error occured in API call `api/map/filters`. The error:")
        console.log(e)
      }
    },
  }
</script>

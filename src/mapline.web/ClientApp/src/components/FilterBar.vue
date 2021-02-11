<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    #filter-bar {
        direction: inline-block;
    }
</style>

<template>
    <div id="filter-bar-template">
        <p>{{ headerText }}</p>
        <div v-for="(item, index) in filters" :key="index">
            <input type="checkbox" :id="item.name" v-model="item.checked" />
            <label :for="item.name">{{ item.name }}</label>

        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                headerText: "Filters:",
                filters: [ ]
            };
        },

        name: 'filter-bar',
        async created() {
            try {
                this.loading = true;

                this.filters = [];
                const response = await this.$axios.get('api/map/filters');
                this.filters = response.data;

                console.log("filters after api call:");
                console.log(this.filters);

            } catch (e) {
                alert("An unexpected error occured in API handling...")
                console.log("An unexpected error occured in API handling. The error:")
                console.log(e)
            }
        },
    }
</script>

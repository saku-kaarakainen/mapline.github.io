<style scoped>
    #map-control-editor {
    }
</style>
<template>
    <div id="map-control-editor">
        <p>This is a component</p>
    </div>
</template>

<script>
    export default {
        data() {
            return {

            };
        },

        name: 'map-control-editor',

        methods: {

        },

        async created() {
            try {
                console.log("called components/MapControlEditor.vue/async created");
            } catch (e) {
                alert("An unexpected error occuurred in components/MapControlEditor.vue/async created.");
                console.log("An unexpected error occuurred in components/MapControlEditor.vue/async created. The error:");
                console.log(e);
            }
        }
    }
</script>
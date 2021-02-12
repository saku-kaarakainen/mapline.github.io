<style scoped>
    #slider-template {
        padding-right: 10px;
    }

    .stable-slider, .cell-slider, table, tr {
        width: 100%;
    }

    #ranged-slider {
        width: 90%;
    }

</style>
<template>
    <div id="slider-template">
        <!-- to fill the area with the slider: https://stackoverflow.com/questions/773517/style-input-element-to-fill-remaining-width-of-its-container
        It's rather ugly, but working solution... -->
        <table class="table-slider">
            <tr>
                <td class="cell-text" v-bind:style="{ width: headerWith }">
                    <label id="slider-header" for="ranged-slider">{{ sliderHeader }}</label>
                    <span id="current-value">{{ current }}</span>
                </td>
                <td class="cell-slider">
                    <!-- TODO: Use dynamic min and max - values -->
                    <vue-slider id="ranged-slider"
                           v-model="current"
                           :min="min"
                           :max="max"
                    />
                </td>
            </tr>
        </table>
    </div>
</template>

<script lang="ts">
    import VueSlider from 'vue-slider-component'
    import 'vue-slider-component/theme/antd.css'
    import 'vue-slider-component/theme/default.css'

    export default {
        name: 'slider',
        components: {
            VueSlider
        },

        data() {
            return {
                sliderHeader: "Year:",
                headerWith: '10%',
                current: -7000,
                timer: '',
                min: -7000,
                max: 2021
            };
        },
        
        async created() {
            try {
                // updates every second
                this.timer = setInterval(this.updateTimer, 1000);
            } catch (e) {
                alert("An unexpected error occuurred in components/slider.vue/async created.");
                console.log("An unexpected error occuurred in components/slider.vue/async created. The error:");
                console.log(e);
            }
        },

        methods: {
            updateTimer: function () {
                this.current++;
            },
            cancelAutoUpdate() {
                clearInterval(this.timer);
            }
        },

        beforeDestroy() {
            clearInterval(this.timer);
        }
    }
</script>
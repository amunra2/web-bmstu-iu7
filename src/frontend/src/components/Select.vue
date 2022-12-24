<template>
    <select class="select" :style="{fontSize}" v-model="selected">
      <option v-for="option in options" :value="option" v-bind:key="option">{{option}}</option>
    </select>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "Select",
  components: {},
  props: ['fontSize'],
  data() {
    return {
      selected: '',
      options: [
        'A',
        'B',
        'C'
      ]
    }
  }
});
</script>

<style scoped>
  .select {
    background: transparent;
    background-image: url("data:image/svg+xml;charset=utf8,%3Csvg xmlns='http://www.w3.org/2000/svg' version='1.1' height='15px' width='15px'%3E%3Ctext x='0' y='10' fill='cyan'%3E%E2%96%BE%3C/text%3E%3C/svg%3E");
    background-repeat: no-repeat;
    background-size: 1.5em 1.5em;
    background-position: right center;
    /* background-clip: border-box; */
    border: none;
    border-bottom: 2px solid var(--cyan);;
    width: 251px;
    height: 35px;
    filter: drop-shadow(0px 0px 10px var(--magenta));
    font: "Chakra Petch";
    color: var(--cyan);
    appearance: none;
  }
</style>

